
using System.Collections.Generic;

namespace TurnBasedBattle.Model.Commands.Abstract
{
    public abstract class BaseCommand : ICommand
    {
        public CommandStatus Status { get; private set; }

        protected List<ICommand> Children { get; } = new List<ICommand>();

        IEnumerable<ICommand> ICommand.Children => Children;
    
        void ICommand.Execute()
        {
            Children.Clear();
            Status = OnExecute();
        }

        protected abstract CommandStatus OnExecute();

        protected static CommandStatus Success() => CommandStatus.Success;
    
        protected static CommandStatus Fail() => CommandStatus.Failed;
    }
}