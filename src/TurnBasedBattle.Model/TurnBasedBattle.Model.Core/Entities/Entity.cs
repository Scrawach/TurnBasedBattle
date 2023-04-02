using TurnBasedBattle.Model.Core.Components;
using TurnBasedBattle.Model.Core.Entities.Abstract;

namespace TurnBasedBattle.Model.Core.Entities;

public sealed class Entity : IEntity
{
    private readonly Dictionary<Type, IComponent> _components;

    public Entity() => 
        _components = new Dictionary<Type, IComponent>();

    public bool Has<TComponent>() where TComponent : IComponent => 
        _components.ContainsKey(typeof(TComponent));

    public bool TryGet<TComponent>(out TComponent component) where TComponent : IComponent
    {
        component = default;

        if (!Has<TComponent>())
            return false;

        component = Get<TComponent>();
        return true;
    }

    public TComponent Get<TComponent>() where TComponent : IComponent => 
        (TComponent) _components[typeof(TComponent)];

    public IEntity Add<TComponent>(TComponent component) where TComponent : IComponent
    {
        _components.Add(typeof(TComponent), component);
        return this;
    }

    public override string? ToString() =>
        Has<UniqueId>()
            ? Get<UniqueId>().ToString()
            : base.ToString();
}