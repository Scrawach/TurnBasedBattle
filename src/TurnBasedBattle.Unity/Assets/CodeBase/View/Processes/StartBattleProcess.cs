using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Attributes;
using CodeBase.View.Characters;
using CodeBase.View.Characters.Services;
using CodeBase.View.Environment;
using CodeBase.View.Factory;
using CodeBase.View.Processes.Abstract;
using TurnBasedBattle.Model.Battle.Commands;
using TurnBasedBattle.Model.Core.Entities.Abstract;
using TurnBasedBattle.Model.EventBus.Abstract;
using UnityEngine;

namespace CodeBase.View.Processes
{
    [ViewProcess(typeof(StartBattle))]
    public class StartBattleProcess : ViewProcess, IStartEventListener<StartBattle>, IDoneEventListener<StartBattle>
    {
        private readonly IGameFactory _gameFactory;
        private readonly IArenaFactory _arenaFactory;
        private readonly IGameObjectProvider _gameObjects;

        private IEntity _previousEnemy;

        public StartBattleProcess(IGameFactory gameFactory, IArenaFactory arenaFactory, IGameObjectProvider gameObjects)
        {
            _gameFactory = gameFactory;
            _arenaFactory = arenaFactory;
            _gameObjects = gameObjects;
        }
        
        public void OnStart(StartBattle battle) =>
            Process(token => PreparingForBattle(battle, token));

        public void OnDone(StartBattle battle) =>
            Process(CleanUpEnvironment);

        private async Task CleanUpEnvironment(CancellationToken token) =>
            _arenaFactory.ClearPrevious();

        private async Task PreparingForBattle(StartBattle battle, CancellationToken token = default)
        {
            _arenaFactory.CreateNextArena();
            var playerPoint = _arenaFactory.NextPlayerPoint();
            var enemyPoint = _arenaFactory.NextEnemyPoint();
            
            var hero = CreatePlayer(battle.Player);
            CreateEnemy(battle.Enemy, at: enemyPoint, lookAt: playerPoint);

            var cameraMoving = MoveCamera(playerPoint, 4f, token);
            await hero.MoveAsync(at: playerPoint, token);
            await cameraMoving;

            if (_previousEnemy != null)
                _gameObjects.Destroy(_previousEnemy.ToString());
            _previousEnemy = battle.Enemy;
        }

        private Character CreatePlayer(IEntity player)
        {
            var id = player.ToString();

            return _gameObjects.Has(id) 
                ? _gameObjects[id] 
                : _gameFactory.CreateFrom(player, at: new Vector3(0, 0, -20));
        }

        private Character CreateEnemy(IEntity enemy, Vector3 at, Vector3 lookAt)
        {
            var view = _gameFactory.CreateFrom(enemy, at);
            var lookAtPlayer = (lookAt - at).normalized;
            view.transform.forward = lookAtPlayer;
            return view;
        }
        
        private static async Task MoveCamera(Vector3 viewPoint, float speed, CancellationToken token = default)
        {
            var camera = Camera.main;
            var startPosition = camera.transform.position;
            var targetPosition = new Vector3(startPosition.x, startPosition.y, viewPoint.z);
            var distance = Vector3.Distance(startPosition, targetPosition);
            var countOfSteps = distance / speed;
            var progress = 0f;
            
            while (progress < 1f)
            {
                progress += Time.deltaTime / countOfSteps;
                camera.transform.position = Vector3.Lerp(startPosition, targetPosition, progress);
                
                await Task.Yield();
                token.ThrowIfCancellationRequested();
            }

            camera.transform.position = targetPosition;
        }
    }
}