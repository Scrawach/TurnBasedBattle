using System;
using System.Threading.Tasks;
using CodeBase.DebugServices;
using CodeBase.View.AssetManagement;
using CodeBase.View.Characters.Services;
using CodeBase.View.Environment;
using CodeBase.View.Factory;
using CodeBase.View.Processes.Services;
using TurnBasedBattle.Model.Battle.Abstract;
using TurnBasedBattle.Model.Battle.Services;
using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.Commands.Services;
using TurnBasedBattle.Model.Core.Services.Characters;
using TurnBasedBattle.Model.EventBus;
using TurnBasedBattle.Model.EventBus.Abstract;
using UnityEngine;

namespace CodeBase
{
    public class BattleView : IView, IDisposable
    {
        private ViewExecutor _viewExecutor;
            
        public async Task Process()
        {
            var eventBus = new DebugEventBus(new EventBus<ICommand>(), Debug.unityLogger);
            var characters = new CharacterRegistry();
            
            var mechanics = new CoreMechanics(characters);
            var executor = new CommandExecutor(eventBus, mechanics);

            _viewExecutor = CreateExecutor(eventBus);
            
            var turnBasedBattle = new TurnBasedBattle.Model.Battle.TurnBasedBattle(executor, mechanics, this);
            await turnBasedBattle.Process();
            
            Debug.Log("Battle is over!");
        }

        public async Task Update() =>
            await _viewExecutor.Execute();

        public void Dispose() =>
            _viewExecutor?.Dispose();

        private static ViewExecutor CreateExecutor(IEventBus<ICommand> eventBus) =>
            new ViewExecutorBuilder()
                .Bind<IAssets, Assets>()
                .Bind<IGameObjectProvider, GameObjectProvider>()
                .Bind<IGameFactory, GameFactory>()
                .Bind<IUIFactory, UIFactory>()
                .Bind<IArenaFactory, ArenaFactory>()
                .Build(eventBus);
    }
}