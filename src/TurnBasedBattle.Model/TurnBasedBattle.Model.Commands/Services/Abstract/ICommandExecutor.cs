using TurnBasedBattle.Model.Commands.Abstract;

namespace TurnBasedBattle.Model.Commands.Services.Abstract
{
    public interface ICommandExecutor
    {
        void Execute(ICommand command);
    }
}