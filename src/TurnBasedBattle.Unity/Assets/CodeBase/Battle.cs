using System;
using System.Threading.Tasks;
using CodeBase.DebugServices;
using CodeBase.View.AssetManagement;
using CodeBase.View.Characters.Services;
using CodeBase.View.Processes.Services;
using TurnBasedBattle.Model.Battle.Abstract;
using TurnBasedBattle.Model.Battle.Services;
using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.Commands.Services;
using TurnBasedBattle.Model.Core.Services.Characters;
using TurnBasedBattle.Model.EventBus;
using UnityEngine;

namespace CodeBase
{
    public class Battle : IView, IDisposable
    {
        private ViewExecutor _viewExecutor;
            
        public async Task Process()
        {
            var eventBus = new DebugEventBus(new EventBus<ICommand>(), Debug.unityLogger);
            var assets = new Assets();
            var gameObjects = new GameObjectProvider();
            var viewProcessBinder = new ViewProcessBinder(eventBus, gameObjects, assets);

            var characters = new CharacterRegistry();
            var mechanics = new CoreMechanics(characters);
            var executor = new CommandExecutor(eventBus, mechanics);
            
            viewProcessBinder.BindAll();
            _viewExecutor = viewProcessBinder.Executor();
            
            var turnBasedBattle = new TurnBasedBattle.Model.Battle.TurnBasedBattle(executor, mechanics, this);
            await turnBasedBattle.Process();
            
            viewProcessBinder.UnbindAll();
            
            Debug.Log("Simulation is over!");
        }

        public async Task Update()
        {
            await _viewExecutor.Execute();
            await Task.Delay(100);
        }

        public void Dispose() =>
            _viewExecutor?.Dispose();
    }
}