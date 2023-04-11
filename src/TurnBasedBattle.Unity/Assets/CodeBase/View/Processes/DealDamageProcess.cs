using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Attributes;
using CodeBase.View.Characters.Services;
using CodeBase.View.Factory;
using CodeBase.View.Processes.Abstract;
using TurnBasedBattle.Model.Commands.Implementations;
using TurnBasedBattle.Model.Core.Components;
using TurnBasedBattle.Model.EventBus.Abstract;

namespace CodeBase.View.Processes
{
    [ViewProcess(typeof(DealDamage))]
    public class DealDamageProcess : ViewProcess, IStartEventListener<DealDamage>
    {
        private readonly IGameObjectProvider _gameObjects;
        private readonly IUIFactory _uiFactory;

        public DealDamageProcess(IGameObjectProvider gameObjects, IUIFactory uiFactory)
        {
            _gameObjects = gameObjects;
            _uiFactory = uiFactory;
        }

        public void OnStart(DealDamage damage)
        {
            var health = damage.Target.Get<Health>();
            var current = health.Value;
            var total = health.Total;
            var id = damage.Target.Get<UniqueId>().ToString();
            
            Process(token => DealDamageAsync(id, damage.Damage, current, total, token));
        }

        private async Task DealDamageAsync(string id, int damage, int current, int total, CancellationToken token = default)
        {
            var defender = _gameObjects[id];
            _uiFactory.CreateDamageBubble(defender.transform.position, damage);

            if (current != 0)
            {
                defender.HealthBar.UpdateValue(current, total, 1.0f, token);
                await defender.HitAsync(token);
            }
            else
            {
                defender.HealthBar.UpdateValue(current, total, 0.5f, token);
            }
        }
    }
}