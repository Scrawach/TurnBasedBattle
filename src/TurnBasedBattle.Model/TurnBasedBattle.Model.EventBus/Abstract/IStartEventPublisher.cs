namespace TurnBasedBattle.Model.EventBus.Abstract
{
    public interface IStartEventPublisher<in TConstraint>
    {
        void Start<TEvent>(TEvent args) where TEvent : TConstraint;
    }
}