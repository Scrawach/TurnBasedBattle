using TurnBasedBattle.Model.Core.Entities.Abstract;

namespace TurnBasedBattle.Model.Core.Components
{
    public sealed class UniqueId : IComponent
    {
        public UniqueId(string value) => 
            Value = value;

        public string Value { get; }

        public override string ToString() => 
            $"{Value}";
    }
}