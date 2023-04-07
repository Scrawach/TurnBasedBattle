using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.EventBus.Abstract;
using UnityEngine;

namespace CodeBase.DebugServices
{
    public class DebugEventBus : IEventBus<ICommand>
    {
        private readonly IEventBus<ICommand> _origin;
        private readonly ILogger _logger;

        public DebugEventBus(IEventBus<ICommand> origin, ILogger logger)
        {
            _origin = origin;
            _logger = logger;
        }
        
        public void Subscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : ICommand
        {
            _logger.Log($"[Event bus] subscribe {listener} to {typeof(TEvent)}");
            _origin.Subscribe(listener);
        }

        public void Unsubscribe<TEvent>(IEventListener<TEvent> listener) where TEvent : ICommand
        {
            _logger.Log($"[Event bus] unsubscribe {listener} from {typeof(TEvent)}");
            _origin.Unsubscribe(listener);
        }

        public void Start<TEvent>(TEvent args) where TEvent : ICommand
        {
            _logger.Log($"[Event bus][Start] publish: {args} ({args.Status})");
            _origin.Start(args);
        }

        public void Done<TEvent>(TEvent args) where TEvent : ICommand
        {
            _origin.Done(args);
        }
    }
}