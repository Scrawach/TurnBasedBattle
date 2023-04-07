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
    [ViewProcess(typeof(HealDamage))]
    public class HealDamageProcess : ViewProcess, IStartEventListener<HealDamage>
    {
        private readonly IGameObjectProvider _gameObjects;

        public HealDamageProcess(IGameObjectProvider gameObjects) =>
            _gameObjects = gameObjects;
        
        public void OnStart(HealDamage heal)
        {
            var health = heal.Target.Get<Health>();
            var current = health.Value;
            var total = health.Total;
            
            Process(token => HealAsync(heal.Target.ToString(), current, total, token));
        }
        
        private async Task HealAsync(string id, int currentHealth, int totalHealth, CancellationToken token = default)
        {
            var defender = _gameObjects[id];
            defender.SetHealthBarAsync(currentHealth, totalHealth, 1.0f, token);
        }
    }
}