using TurnBasedBattle.Model.Core.Entities.Abstract;

namespace TurnBasedBattle.Model.Core.Components
{
    public sealed class Mana : IComponent
    {
        public Mana(int total) : this(total, total) { }
    
        public Mana(int value, int total)
        {
            Value = value;
            Total = total;
        }

        public int Value { get; set; }
        public int Total { get; set; }

        public override string ToString() => 
            $"Mana = {Value} / {Total}";
    }
}