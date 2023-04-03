namespace TurnBasedBattle.Model.EventBus.Abstract
{
    public interface IEventObserver<in TConstraint>
    {
        void Subscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : TConstraint;
        void Unsubscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : TConstraint;
    }
}