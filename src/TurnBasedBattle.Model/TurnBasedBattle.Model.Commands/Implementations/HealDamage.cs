using System;
using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.Core.Components;
using TurnBasedBattle.Model.Core.Entities.Abstract;
using TurnBasedBattle.Model.Core.Extensions;

public sealed class HealDamage : BaseCommand
{
    public readonly IEntity Target;
    public readonly int Power;

    public HealDamage(IEntity target, int power)
    {
        Target = target;
        Power = power;
    }

    protected override CommandStatus OnExecute()
    {
        if (Target.HasNot<Health>())
            return Fail();

        var health = Target.Get<Health>();
        health.Value = Math.Min(health.Value + Power, health.Total);
        return Success();
    }

    public override string ToString() => 
        $"{Target} heals {Power} healths";
}