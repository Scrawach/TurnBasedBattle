using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.AssetManagement;
using CodeBase.View.Attributes;
using CodeBase.View.Characters;
using CodeBase.View.Characters.Services;
using CodeBase.View.Processes.Abstract;
using TurnBasedBattle.Model.Battle.Commands;
using TurnBasedBattle.Model.Core.Components;
using TurnBasedBattle.Model.EventBus.Abstract;
using UnityEngine;

namespace CodeBase.View.Processes
{
    [ViewProcess(typeof(Spawn))]
    public class SpawnProcess : ViewProcess, IStartEventListener<Spawn>
    {
        private const string PrefabPath = "DogKnight/Knight";
        
        private readonly IGameObjectProvider _gameObjects;
        private readonly IAssets _assets;

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
            var gameObject = _assets.Instantiate<Character>(PrefabPath);
            _gameObjects[heroId] = gameObject;

            if (heroEntity.Get<TeamMarker>().TeamId == 0) 
                _gameObjects[heroId].transform.position -= new Vector3(0, 0, 20);
        }
    }
}