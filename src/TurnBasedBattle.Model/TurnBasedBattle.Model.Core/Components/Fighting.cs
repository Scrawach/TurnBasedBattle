using TurnBasedBattle.Model.Core.Entities.Abstract;

namespace TurnBasedBattle.Model.Core.Components
{
    public sealed class Fighting : IComponent
    {
        public Fighting(int power) => 
            Power = power;

        public int Power { get; set; }

        public override string ToString() => 
            $"{nameof(Fighting)} = {Power}";
    }
}