namespace TurnBasedBattle.Model.EventBus.Abstract
{
    public interface IDoneEventListener<in TEvent> : IEventListener<TEvent>
    {
        void OnDone(TEvent args);
    }
}