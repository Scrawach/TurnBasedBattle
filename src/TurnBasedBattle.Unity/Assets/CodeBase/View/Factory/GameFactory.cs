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

        public GameObject CreateKnightFrom(IEntity entity)
        {
            var prefab = _assets.Instantiate<GameObject>(AssetPath.Knight);
            _gameObjects[entity.ToString()] = prefab.GetComponent<Character>();
            return prefab;
        }
    }
}