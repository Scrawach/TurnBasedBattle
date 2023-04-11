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
    [ViewProcess(typeof(DealDamage))]
    public class DealDamageProcess : ViewProcess, IStartEventListener<DealDamage>
    {
        private readonly IGameObjectProvider _gameObjects;

        public DealDamageProcess(IGameObjectProvider gameObjects) =>
            _gameObjects = gameObjects;
        
        public void OnStart(DealDamage damage)
        {
            var health = damage.Target.Get<Health>();
            var current = health.Value;
            var total = health.Total;
            var id = damage.Target.Get<UniqueId>().ToString();
            
            Process(token => DealDamageAsync(id, current, total, token));
        }

        private async Task DealDamageAsync(string id, int current, int total, CancellationToken token = default)
        {
            var defender = _gameObjects[id];

            if (current != 0)
            {
                defender.HealthBar.Show(current, total, 1.0f, token);
                await defender.HitAsync(token);
            }
            else
            {
                defender.HealthBar.Show(current, total, 0.5f, token);
            }
        }
    }
}