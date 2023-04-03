using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.Core.Entities.Abstract;

namespace TurnBasedBattle.Model.Commands.Implementations
{
    public sealed class VampireHit : BaseCommand
    {
        public readonly IEntity Attacker;
        public readonly IEntity Defender;
        public readonly int Power;

        public VampireHit(IEntity attacker, IEntity defender, int power)
        {
            Attacker = attacker;
            Defender = defender;
            Power = power;
        }

        protected override CommandStatus OnExecute()
        {
            Children.Add(new DealDamage(Defender, Power));
            Children.Add(new HealDamage(Attacker, Power));
            return Success();
        }
    }
}