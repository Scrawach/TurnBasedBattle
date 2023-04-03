namespace TurnBasedBattle.Model.EventBus.Abstract
{
    public interface IEventBus<in TConstraint> : IEventObserver<TConstraint>, IStartEventPublisher<TConstraint>, 
        IDoneEventPublisher<TConstraint>
    { }
}