using CodeBase.View.AssetManagement;
using CodeBase.View.Characters;
using CodeBase.View.Characters.Services;
using TurnBasedBattle.Model.Core.Entities.Abstract;
using UnityEngine;

namespace CodeBase.View.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAssets _assets;
        private readonly IGameObjectProvider _gameObjects;

        public GameFactory(IAssets assets, IGameObjectProvider gameObjects)
        {
            _assets = assets;
            _gameObjects = gameObjects;
        }

        public Character CreateFrom(IEntity entity, Vector3 at)
        {
            var prefab = _assets.Instantiate<Character>(AssetPath.Knight, at);
            _gameObjects[entity.ToString()] = prefab;
            return prefab;
        }
    }
}