using System;
using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.Core.Components;
using TurnBasedBattle.Model.Core.Entities.Abstract;
using TurnBasedBattle.Model.Core.Extensions;

namespace TurnBasedBattle.Model.Commands.Implementations
{
    public sealed class DealDamage : BaseCommand
    {
        public readonly IEntity Target;
        public readonly int Damage;

        public DealDamage(IEntity target, int damage)
        {
            Target = target;
            Damage = damage;
        }

        protected override CommandStatus OnExecute()
        {
            if (Target.HasNot<Health>())
                return Fail();
        
            var health = Target.Get<Health>();
            health.Value = Math.Max(0, health.Value - Damage);
            
            if (health.Value == 0)
                Children.Add(new DieCommand(Target));
            
            return Success();
        }

        public override string ToString() => 
            $"{Target} takes {Damage} damage. {Target.Get<Health>()}";
    }
}