using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Attributes;
using CodeBase.View.Characters.Services;
using CodeBase.View.Processes.Abstract;
using TurnBasedBattle.Model.Battle.Commands;
using TurnBasedBattle.Model.EventBus.Abstract;

namespace CodeBase.View.Processes
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
            Process(token => WaitingInitiativeBarFilledAsync(entity.ToString(), token));
        }

        private async Task WaitingInitiativeBarFilledAsync(string entityId, CancellationToken token = default)
        {
            var view = _gameObjects[entityId];
            await view.InitiativeBar.Current;
        }
    }
}