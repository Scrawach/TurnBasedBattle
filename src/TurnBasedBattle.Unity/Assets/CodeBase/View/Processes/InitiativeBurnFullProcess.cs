using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Attributes;
using CodeBase.View.Characters.Services;
using CodeBase.View.Processes.Abstract;
using TurnBasedBattle.Model.Commands.Implementations;
using TurnBasedBattle.Model.Core.Components;
using TurnBasedBattle.Model.EventBus.Abstract;

namespace CodeBase.View.Processes
{
    [ViewProcess(typeof(InitiativeBurn))]
    public class InitiativeBurnFullProcess : ViewProcess, IStartEventListener<InitiativeBurn>
    {
        private readonly IGameObjectProvider _gameObject;

        public InitiativeBurnFullProcess(IGameObjectProvider gameObject) =>
            _gameObject = gameObject;
        
        public void OnStart(InitiativeBurn burn)
        {
            var entity = burn.Target;
            var value = entity.Get<Initiative>().Value;
            var total = entity.Get<Initiative>().Total;
            
            Process(token => BurnAsync(entity.ToString(), value, total, token));
        }

        private async Task BurnAsync(string id, int value, int total, CancellationToken token = default)
        {
            var view = _gameObject[id];
            await view.SetInitiativeBarAsync(value, total, 1.0f, token);
        }
    }
}