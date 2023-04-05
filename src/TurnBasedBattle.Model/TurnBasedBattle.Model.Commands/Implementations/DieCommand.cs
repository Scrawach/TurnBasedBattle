using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.Core.Entities.Abstract;

namespace TurnBasedBattle.Model.Commands.Implementations
{
    public sealed class DieCommand : BaseCommand
    {
        public readonly IEntity Target;

        public DieCommand(IEntity target) =>
            Target = target;
        
        protected override CommandStatus OnExecute() =>
            Success();

        public override string ToString() =>
            $"{Target} died";
    }
}