using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Attributes;
using CodeBase.View.Characters.Services;
using CodeBase.View.Processors.Abstract;
using TurnBasedBattle.Model.Battle.Commands;
using TurnBasedBattle.Model.Core.Components;
using TurnBasedBattle.Model.Core.Entities.Abstract;
using TurnBasedBattle.Model.EventBus.Abstract;

namespace CodeBase.View.Processors
{
    [ViewProcess(typeof(StartTurn))]
    public class StartTurnProcess : ViewProcess, IStartEventListener<StartTurn>
    {
        private readonly IGameObjectProvider _gameObjects;

        public StartTurnProcess(IGameObjectProvider gameObjects) =>
            _gameObjects = gameObjects;

        public void OnStart(StartTurn args)
        {
            var entity = args.Target;
            var value = entity.Get<Initiative>().Value;
            var total = entity.Get<Initiative>().Total;

            Process(token => UpdateInitiativeAsync(entity, value, total, token));
        }

        private async Task UpdateInitiativeAsync(IEntity entity, int value, int total, CancellationToken token = default)
        {
            var view = _gameObjects[entity.ToString()];
            await view.SetInitiativeBarAsync(value, total, 0.5f, token);
        }
    }
}