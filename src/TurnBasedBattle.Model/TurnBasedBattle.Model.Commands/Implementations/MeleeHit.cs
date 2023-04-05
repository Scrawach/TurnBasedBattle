using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.Core.Components;
using TurnBasedBattle.Model.Core.Entities.Abstract;
using TurnBasedBattle.Model.Core.Extensions;

namespace TurnBasedBattle.Model.Commands.Implementations
{
    public sealed class MeleeHit : BaseCommand
    {
        public readonly IEntity Attacker;
        public readonly IEntity Defender;

        public MeleeHit(IEntity attacker, IEntity defender)
        {
            Attacker = attacker;
            Defender = defender;
        }

        protected override CommandStatus OnExecute()
        {
            if (Attacker.HasNot<Fighting>())
                return Fail();

            var fighting = Attacker.Get<Fighting>();
            Children.Add(new DealDamage(Defender, fighting.Power));
        
            if (Defender.Has<CounterAttack>())
                Children.Add(new MeleeHit(Defender, Attacker));
        
            return Success();
        }

        public override string ToString() => 
            $"{Attacker} attack {Defender} in melee";
    }
}