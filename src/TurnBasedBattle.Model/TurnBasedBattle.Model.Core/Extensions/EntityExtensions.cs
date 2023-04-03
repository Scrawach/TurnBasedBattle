using TurnBasedBattle.Model.Core.Entities.Abstract;

namespace TurnBasedBattle.Model.Core.Extensions
{
    public static class EntityExtensions
    {
        public static bool HasNot<TComponent>(this IEntity entity) where TComponent : IComponent => 
            !entity.Has<TComponent>();
    }
}