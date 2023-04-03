using TurnBasedBattle.Model.Core.Entities.Abstract;

namespace TurnBasedBattle.Model.Core.Factory.Abstract
{
    public interface IFactory
    {
        IEntity Create();
    }
}