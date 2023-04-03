using System;
using System.Collections.Generic;
using TurnBasedBattle.Model.EventBus.Abstract;

namespace TurnBasedBattle.Model.EventBus
{
    public sealed class EventBus<TConstraint> : IEventBus<TConstraint>
    {
        private readonly IDictionary<Type, IEventBus<TConstraint>> _containers;

        public EventBus() =>
            _containers = new Dictionary<Type, IEventBus<TConstraint>>();

        public void Start<TEvent>(TEvent args) where TEvent : TConstraint => 
            Container<TEvent>(args.GetType()).Start(args);

        public void Done<TEvent>(TEvent args) where TEvent : TConstraint => 
            Container<TEvent>(args.GetType()).Done(args);

        public void Subscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : TConstraint =>
            Container<TEvent>().Subscribe(listener);

        public void Unsubscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : TConstraint =>
            Container<TEvent>().Unsubscribe(listener);

        private IEventBus<TConstraint> Container<TEvent>() =>
            Container<TEvent>(typeof(TEvent));

        private IEventBus<TConstraint> Container<TEvent>(Type type)
        {
            if (!_containers.TryGetValue(type, out var container))
                _containers.Add(type, container = new ListenerContainer<TEvent, TConstraint>());
            return container;
        }
    }
}