using System.Collections.Generic;

namespace TurnBasedBattle.Model.Commands.Abstract
{
    public interface ICommand
    {
        CommandStatus Status { get; }
        IEnumerable<ICommand> Children { get; }
        void Execute();
    }
}