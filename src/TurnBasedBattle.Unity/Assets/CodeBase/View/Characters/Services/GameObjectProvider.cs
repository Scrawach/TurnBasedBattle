using System.Collections.Generic;

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

        public void Remove(string id) =>
            _gameObjects.Remove(id);
    }
}