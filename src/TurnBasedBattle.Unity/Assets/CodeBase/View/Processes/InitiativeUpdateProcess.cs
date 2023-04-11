using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Attributes;
using CodeBase.View.Characters.Services;
using CodeBase.View.Processes.Abstract;
using JetBrains.Annotations;
using TurnBasedBattle.Model.Commands.Implementations;
using TurnBasedBattle.Model.Core.Components;
using TurnBasedBattle.Model.Core.Entities.Abstract;
using TurnBasedBattle.Model.EventBus.Abstract;

namespace CodeBase.View.Processes
{
    [UsedImplicitly]
    [ViewProcess(typeof(IncreaseInitiative))]
    [ViewProcess(typeof(InitiativeBurn))]
    public class InitiativeUpdateProcess : ViewProcess, IStartEventListener<IncreaseInitiative>, IStartEventListener<InitiativeBurn>
    {
        private readonly IGameObjectProvider _gameObjects;

        public InitiativeUpdateProcess(IGameObjectProvider gameObjects) =>
            _gameObjects = gameObjects;
        
        public void OnStart(IncreaseInitiative increase)
        {
            var (id, current, total) = InitiativeValuesFrom(increase.Target);
            UpdateInitiativeValue(id, current, total);
        }

        public void OnStart(InitiativeBurn burn)
        {
            var (id, current, total) = InitiativeValuesFrom(burn.Target);
            UpdateInitiativeValue(id, current, total);
        }

        private void UpdateInitiativeValue(string id, int current, int total) =>
            Process(token => Updating(id, current, total, token), false);

        private (string id, int current, int total) InitiativeValuesFrom(IEntity entity)
        {
            var initiative = entity.Get<Initiative>();
            return (entity.ToString(), initiative.Value, initiative.Total);
        }

        private async Task Updating(string entityId, int current, int total, CancellationToken token)
        {
            var view = _gameObjects[entityId];
            await view.InitiativeBar.Show(current, total, 1f, token);
        }
    }
}