using System.Collections.Generic;
using UnityEngine;

namespace CodeBase.View.Characters.Services
{
    public class GameObjectProvider : IGameObjectProvider
    {
        private readonly IDictionary<string, Character> _gameObjects;

        public GameObjectProvider() =>
            _gameObjects = new Dictionary<string, Character>();

        public Character this[string id]
        {
            get => _gameObjects[id];
            set => _gameObjects[id] = value;
        }

        public bool Has(string id) =>
            _gameObjects.ContainsKey(id);

        public void Remove(string id) =>
            _gameObjects.Remove(id);

        public void Destroy(string id)
        {
            var view = _gameObjects[id];
            _gameObjects.Remove(id);
            Object.Destroy(view);
        }
    }
}