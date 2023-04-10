using UnityEngine;

namespace CodeBase.View.Environment
{
    public interface IArenaFactory
    {
        Vector3 NextPlayerPoint();
        Vector3 NextEnemyPoint();
        void CreateNextArena();
        void ClearPrevious();
    }
}