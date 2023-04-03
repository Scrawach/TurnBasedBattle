namespace TurnBasedBattle.Model.EventBus.Abstract
{
    public interface IStartEventListener<in TEvent> : IEventListener<TEvent>
    {
        void OnStart(TEvent args);
    }
}