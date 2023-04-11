using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Attributes;
using CodeBase.View.Factory;
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
        private readonly IGameFactory _factory;

        public SpawnProcess(IGameFactory factory) =>
            _factory = factory;

        public void OnStart(Spawn spawn) =>
            Process(token => SpawnAsync(spawn, token));

        private async Task SpawnAsync(Spawn spawn, CancellationToken token = default)
        {
            var heroEntity = spawn.Spawned;
            var gameObject = _factory.CreateKnightFrom(heroEntity);
            if (heroEntity.Get<TeamMarker>().TeamId == 0) 
                gameObject.transform.position -= new Vector3(0, 0, 20);
        }
    }
}