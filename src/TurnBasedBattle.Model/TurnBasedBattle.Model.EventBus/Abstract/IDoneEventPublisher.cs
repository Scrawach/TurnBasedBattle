namespace TurnBasedBattle.Model.EventBus.Abstract
{
    public interface IDoneEventPublisher<in TConstraint>
    {
        void Done<TEvent>(TEvent args) where TEvent : TConstraint;
    }
}