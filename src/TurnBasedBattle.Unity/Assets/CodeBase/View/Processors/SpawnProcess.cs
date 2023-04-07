using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.AssetManagement;
using CodeBase.View.Attributes;
using CodeBase.View.Characters;
using CodeBase.View.Characters.Services;
using CodeBase.View.Processors.Abstract;
using TurnBasedBattle.Model.Battle.Commands;
using TurnBasedBattle.Model.Core.Components;
using TurnBasedBattle.Model.EventBus.Abstract;
using UnityEngine;

namespace CodeBase.View.Processors
{
    [ViewProcess(typeof(Spawn))]
    public class SpawnProcess : ViewProcess, IStartEventListener<Spawn>
    {
        private const string PrefabPath = "DogKnight/Knight";
        
        private readonly IGameObjectProvider _gameObjects;
        private readonly IAssets _assets;
        
        private Vector3 _playerSpawnPoint = new Vector3(-4, 0, 0);
        private Vector3 _enemySpawnPoint = new Vector3(4, 0, 0);

        public SpawnProcess(IGameObjectProvider gameObjects, IAssets assets)
        {
            _gameObjects = gameObjects;
            _assets = assets;
        }
        
        public void OnStart(Spawn spawn) =>
            Process(token => SpawnAsync(spawn, token));

        private async Task SpawnAsync(Spawn spawn, CancellationToken token = default)
        {
            var heroEntity = spawn.Spawned;
            var heroId = heroEntity.ToString();
            var (spawnPoint, direction) = SpawnData(heroEntity.Get<TeamMarker>().TeamId);
            var gameObject = _assets.Instantiate<Character>(PrefabPath);
                
            _gameObjects[heroId] = gameObject;
            _gameObjects[heroId].transform.position = spawnPoint;
            _gameObjects[heroId].transform.rotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
        }

        private (Vector3 point, Vector3 direction) SpawnData(int teamId) =>
            teamId == 0
                ? (_playerSpawnPoint, _enemySpawnPoint - _playerSpawnPoint) 
                : (_enemySpawnPoint, _playerSpawnPoint - _enemySpawnPoint);
    }
}