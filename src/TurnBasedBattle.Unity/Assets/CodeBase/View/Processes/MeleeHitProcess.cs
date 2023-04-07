using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Attributes;
using CodeBase.View.Characters;
using CodeBase.View.Characters.Services;
using CodeBase.View.Processes.Abstract;
using TurnBasedBattle.Model.Commands.Implementations;
using TurnBasedBattle.Model.EventBus.Abstract;
using UnityEngine;

namespace CodeBase.View.Processes
{
    [ViewProcess(typeof(MeleeHit))]
    public class MeleeHitProcess : ViewProcess, IStartEventListener<MeleeHit>, IDoneEventListener<MeleeHit>
    {
        private readonly IGameObjectProvider _gameObjects;
        private readonly Stack<Vector3> _startPositions = new Stack<Vector3>();

        public MeleeHitProcess(IGameObjectProvider gameObjects) =>
            _gameObjects = gameObjects;
        
        
        public void OnStart(MeleeHit meleeHit) =>
            Process(token => AttackAsync(meleeHit, token));

        public void OnDone(MeleeHit meleeHit) =>
            Process(token => ReturnToPositionAsync(meleeHit, token));

        private async Task AttackAsync(MeleeHit hit, CancellationToken token = default)
        {
            const float distanceForAttack = 1.5f;
            var (attacker, defender) = Actors(hit);
            var targetPosition = defender.transform.position;
            _startPositions.Push(attacker.transform.position);
            
            await attacker.RotateAsync(to: targetPosition, token);
            await attacker.MoveAsync(at: targetPosition, stoppingDistance: distanceForAttack, token);
            await attacker.AttackAsync(token);
            attacker.ResetToIdleAsync(token);
        }

        private async Task ReturnToPositionAsync(MeleeHit hit, CancellationToken token = default)
        {
            var (attacker, defender) = Actors(hit);
            var startPosition = _startPositions.Pop();

            if (Vector3.Distance(attacker.transform.position, startPosition) < 0.1f)
                return;
            
            await attacker.RotateAsync(to: startPosition, token);
            await attacker.MoveAsync(at: startPosition, 0, token);
            await attacker.RotateAsync(to: defender.transform.position, token);
        }
        
        private (Character attacker, Character defender) Actors(MeleeHit hit) =>
            (_gameObjects[hit.Attacker.ToString()], _gameObjects[hit.Defender.ToString()]);
    }
}