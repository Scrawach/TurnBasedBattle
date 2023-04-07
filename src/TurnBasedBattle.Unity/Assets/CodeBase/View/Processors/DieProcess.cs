using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Attributes;
using CodeBase.View.Characters.Services;
using CodeBase.View.Processors.Abstract;
using TurnBasedBattle.Model.Commands.Implementations;
using TurnBasedBattle.Model.EventBus.Abstract;

namespace CodeBase.View.Processors
{
    [ViewProcess(typeof(DieCommand))]
    public class DieProcess : ViewProcess, IStartEventListener<DieCommand>
    {
        private readonly IGameObjectProvider _gameObject;

        public DieProcess(IGameObjectProvider gameObject) =>
            _gameObject = gameObject;

        public void OnStart(DieCommand die) =>
            Process(token => DieAsync(die.Target.ToString(), token));

        private async Task DieAsync(string id, CancellationToken token = default)
        {
            var gameObject = _gameObject[id];
            await gameObject.DieAsync(token);
        }
    }
}