using CodeBase.View.Characters;
using TurnBasedBattle.Model.Core.Entities.Abstract;
using UnityEngine;

namespace CodeBase.View.Factory
{
    public interface IGameFactory
    {
        Character CreateFrom(IEntity entity, Vector3 at);
    }
}