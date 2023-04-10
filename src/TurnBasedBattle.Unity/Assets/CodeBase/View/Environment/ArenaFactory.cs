using CodeBase.View.AssetManagement;
using UnityEngine;

namespace CodeBase.View.Environment
{
    public class ArenaFactory : IArenaFactory
    {
        private readonly IAssets _assets;
        private Arena _previous;
        private Arena _current;

        public ArenaFactory(IAssets assets) =>
            _assets = assets;

        public Vector3 NextPlayerPoint() =>
            _current.PlayerPoint.position;

        public Vector3 NextEnemyPoint() =>
            _current.EnemyPoint.position;

        public void CreateNextArena()
        {
            _previous = _current;
            var nextArena = _assets.Instantiate<Arena>("Environment/Arena");
            var previousPositionArena = _current == null ? 0 : _current.transform.position.z + _current.Size;
            nextArena.transform.position = new Vector3(0, 0, previousPositionArena);
            _current = nextArena;
        }

        public void ClearPrevious()
        {
            if (_previous != null) 
                Object.Destroy(_previous.gameObject);
        }
    }
}