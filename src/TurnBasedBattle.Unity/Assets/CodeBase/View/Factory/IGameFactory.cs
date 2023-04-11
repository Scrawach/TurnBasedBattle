using TurnBasedBattle.Model.Core.Entities.Abstract;
using UnityEngine;

namespace CodeBase.View.Factory
{
    public interface IGameFactory
    {
        GameObject CreateKnightFrom(IEntity entity);
    }
}