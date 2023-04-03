using TurnBasedBattle.Model.Core.Components;
using TurnBasedBattle.Model.Core.Entities;
using TurnBasedBattle.Model.Core.Entities.Abstract;
using TurnBasedBattle.Model.Core.Factory.Abstract;

namespace TurnBasedBattle.Model.Core.Factory
{
    public sealed class HumanFactory : IFactory
    {
        private readonly int _health;
        private readonly int _mana;
        private readonly int _fighting;

        public HumanFactory(int health, int mana, int fighting)
        {
            _health = health;
            _mana = mana;
            _fighting = fighting;
        }

        public IEntity Create() =>
            new Entity()
                .Add(new Health(_health))
                .Add(new Mana(_mana))
                .Add(new Fighting(_fighting));
    }
}