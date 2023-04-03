namespace TurnBasedBattle.Model.Core.Entities.Abstract
{
    public interface IEntity
    {
        bool Has<TComponent>() where TComponent : IComponent;
        bool TryGet<TComponent>(out TComponent component) where TComponent : IComponent;
        TComponent Get<TComponent>() where TComponent : IComponent;
        IEntity Add<TComponent>(TComponent component) where TComponent : IComponent;
    }
}