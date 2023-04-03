using TurnBasedBattle.Model.Core.Components;
using TurnBasedBattle.Model.Core.Entities.Abstract;
using TurnBasedBattle.Model.Core.Factory.Abstract;

namespace TurnBasedBattle.Model.Core.Factory
{
    public sealed class UniqueFactory : IFactory
    {
        private readonly IFactory _origin;
        private readonly string _prefix;
        private int _counter;

        public UniqueFactory(IFactory origin, string prefix)
        {
            _origin = origin;
            _prefix = prefix;
        }

        public IEntity Create()
        {
            var id = $"{_prefix}-{_counter}";
            _counter++;
        
            return _origin
                .Create()
                .Add(new UniqueId(id));
        }
    }
}