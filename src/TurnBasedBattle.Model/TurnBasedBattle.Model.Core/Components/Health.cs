using TurnBasedBattle.Model.Core.Entities.Abstract;

namespace TurnBasedBattle.Model.Core.Components
{
    public sealed class Health : IComponent
    {
        public Health(int total) : this(total, total) { }
    
        public Health(int value, int total)
        {
            Value = value;
            Total = total;
        }

        public int Value { get; set; }
        public int Total { get; set; }

        public override string ToString() => 
            $"Health = {Value} / {Total}";
    }
}