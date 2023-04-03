using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.Commands.Services.Abstract;
using TurnBasedBattle.Model.EventBus.Abstract;

namespace TurnBasedBattle.Model.Commands.Services
{
    public sealed class CommandExecutor : ICommandExecutor
    {
        private readonly IEventBus<ICommand> _emitter;

        public CommandExecutor(IEventBus<ICommand> emitter) => 
            _emitter = emitter;

        public void Execute(ICommand command)
        {
            command.Execute();
            _emitter.Start(command);
            foreach (var child in command.Children) 
                Execute(child);
            _emitter.Done(command);
        }
    }
}