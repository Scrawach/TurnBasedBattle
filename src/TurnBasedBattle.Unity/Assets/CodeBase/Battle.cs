using System.Threading.Tasks;
using CodeBase.DebugServices;
using CodeBase.View.AssetManagement;
using CodeBase.View.Characters.Services;
using CodeBase.View.Processors.Services;
using TurnBasedBattle.Model.Battle.Abstract;
using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.EventBus;
using UnityEngine;

namespace CodeBase
{
    public class Battle : IView
    {
        private ViewExecutor _executor;
            
        public async Task Process()
        {
            var eventBus = new DebugEventBus(new EventBus<ICommand>(), Debug.unityLogger);
            var assets = new Assets();
            var gameObjects = new GameObjectProvider();
            var viewProcessBinder = new ViewProcessBinder(eventBus, gameObjects, assets);
            
            viewProcessBinder.BindAll();
            _executor = viewProcessBinder.Executor();
            
            var turnBasedBattle = new TurnBasedBattle.Model.Battle.TurnBasedBattle(eventBus, this);
            await turnBasedBattle.Process();
            
            viewProcessBinder.UnbindAll();
            
            Debug.Log("Simulation is over!");
        }

        public async Task Update()
        {
            await _executor.Execute();
            await Task.Delay(100);
        }
    }
}