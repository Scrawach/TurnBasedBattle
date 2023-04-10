using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Attributes;
using CodeBase.View.Characters.Services;
using CodeBase.View.Environment;
using CodeBase.View.Processes.Abstract;
using TurnBasedBattle.Model.Battle.Commands;
using TurnBasedBattle.Model.Core.Services.Characters.Abstract;
using TurnBasedBattle.Model.EventBus.Abstract;
using UnityEngine;

namespace CodeBase.View.Processes
{
    [ViewProcess(typeof(StartBattle))]
    public class StartBattleProcess : ViewProcess, IStartEventListener<StartBattle>, IDoneEventListener<StartBattle>
    {
        private readonly ICharacterProvider _characters;
        private readonly IGameObjectProvider _gameObjects;
        private readonly IArenaFactory _arenaFactory;

        public StartBattleProcess(ICharacterProvider characters, IArenaFactory arenaFactory, IGameObjectProvider gameObjects)
        {
            _characters = characters;
            _arenaFactory = arenaFactory;
            _gameObjects = gameObjects;
        }
        
        public void OnStart(StartBattle args) =>
            Process(PreparingForBattle);

        public void OnDone(StartBattle args) =>
            Process(CleanUpEnvironment);

        private async Task CleanUpEnvironment(CancellationToken token) =>
            _arenaFactory.ClearPrevious();

        private async Task PreparingForBattle(CancellationToken token)
        {
            _arenaFactory.CreateNextArena();
            var enemy = _characters.AlliesOf(1).First();
            var player = _characters.AlliesOf(0).First();

            var enemyPoint = _arenaFactory.NextEnemyPoint();
            var playerPoint = _arenaFactory.NextPlayerPoint();
            
            var fromEnemyToPlayer = (playerPoint - enemyPoint).normalized;

            var enemyView = _gameObjects[enemy.ToString()];
            enemyView.transform.position = enemyPoint;
            enemyView.RotateAsync(fromEnemyToPlayer, token);
            
            var playerView = _gameObjects[player.ToString()];
            var cameraMoving = MoveCamera(playerPoint, 6f, token);
            await playerView.MoveAsync(playerPoint, 0f, token);
            await cameraMoving;
        }

        private static async Task MoveCamera(Vector3 viewPoint, float durationInSeconds, CancellationToken token = default)
        {
            var camera = Camera.main;
            var startPosition = camera.transform.position;
            var targetPosition = new Vector3(startPosition.x, startPosition.y, viewPoint.z);
            var progress = 0f;

            while (progress < durationInSeconds)
            {
                progress += Time.deltaTime;
                camera.transform.position = Vector3.Lerp(startPosition, targetPosition, progress / durationInSeconds);
                await Task.Yield();
                token.ThrowIfCancellationRequested();
            }

            camera.transform.position = targetPosition;
        }
    }
}