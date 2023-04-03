using System.Collections.Generic;
using TurnBasedBattle.Model.EventBus.Abstract;

namespace TurnBasedBattle.Model.EventBus
{
    internal sealed class ListenerContainer<TListener, TConstraint> : IEventBus<TConstraint>
    {
        private readonly ICollection<IStartEventListener<TListener>> _startListener;
        private readonly ICollection<IDoneEventListener<TListener>> _doneListener;

        public ListenerContainer()
        {
            _startListener = new HashSet<IStartEventListener<TListener>>();
            _doneListener = new HashSet<IDoneEventListener<TListener>>();
        }

        public void Subscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : TConstraint
        {
            if (listener is IStartEventListener<TListener> startListener)
                _startListener.Add(startListener);
        
            if (listener is IDoneEventListener<TListener> doneListener)
                _doneListener.Add(doneListener);
        }

        public void Unsubscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : TConstraint
        {
            if (listener is IStartEventListener<TListener> startListener)
                _startListener.Remove(startListener);
        
            if (listener is IDoneEventListener<TListener> doneListener)
                _doneListener.Remove(doneListener);
        }

        public void Start<TEvent>(TEvent args) where TEvent : TConstraint
        {
            var casted = (TListener)(args as object);
            foreach (var listener in _startListener) 
                listener.OnStart(casted);
        }

        public void Done<TEvent>(TEvent args) where TEvent : TConstraint
        {
            var casted = (TListener)(args as object);
            foreach (var listener in _doneListener) 
                listener.OnDone(casted);
        }
    }
}