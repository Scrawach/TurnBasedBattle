using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Attributes;
using CodeBase.View.Characters.Services;
using CodeBase.View.Factory;
using CodeBase.View.Processes.Abstract;
using JetBrains.Annotations;
using TurnBasedBattle.Model.Commands.Implementations;
using TurnBasedBattle.Model.Core.Components;
using TurnBasedBattle.Model.EventBus.Abstract;

namespace CodeBase.View.Processes
{
    [UsedImplicitly]
    [ViewProcess(typeof(HealDamage))]
    public class HealDamageProcess : ViewProcess, IStartEventListener<HealDamage>
    {
        private readonly IGameObjectProvider _gameObjects;
        private readonly IUIFactory _uiFactory;

        public HealDamageProcess(IGameObjectProvider gameObjects, IUIFactory uiFactory)
        {
            _gameObjects = gameObjects;
            _uiFactory = uiFactory;
        }

        public void OnStart(HealDamage heal)
        {
            var health = heal.Target.Get<Health>();
            var current = health.Value;
            var total = health.Total;
            
            Process(token => HealAsync(heal.Target.ToString(), heal.Power, current, total, token));
        }
        
        private async Task HealAsync(string id, int power, int currentHealth, int totalHealth, CancellationToken token = default)
        {
            var defender = _gameObjects[id];
            _uiFactory.CreateHealBubble(defender.transform.position, power);
            await defender.HealthBar.UpdateValue(currentHealth, totalHealth, 1.0f, token);
        }
    }
}