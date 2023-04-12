using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Characters.Components;
using CodeBase.View.UI.Abstract;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.View.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField] private AsyncMover _mover;
        [SerializeField] private AsyncRotator _rotator;

        [field: SerializeField] 
        public AsyncViewValue HealthBar { get; private set; }
        
        [field: SerializeField]
        public AsyncViewValue InitiativeBar { get; private set; }

        public async Task MoveAsync(Vector3 at, CancellationToken token = default) =>
            await MoveAsync(at, 0f, token);
        
        public async Task MoveAsync(Vector3 at, float stoppingDistance, CancellationToken token = default)
        {
            _animator.Play(AnimationHashes.RunHash);
            await _mover.MoveAsync(at, stoppingDistance, token);
            _animator.Play(AnimationHashes.IdleHash);
            await Task.Yield();
        }

        public async Task RotateAsync(Vector3 to, CancellationToken token = default) =>
            await _rotator.RotateAsync(to, token);

        public async Task AttackAsync(CancellationToken token = default)
        {
            var attacks = new[] {AnimationHashes.Attack01Hash, AnimationHashes.Attack02Hash};
            var randomIndex = Random.Range(0, attacks.Length);
            _animator.Play(attacks[randomIndex]);
            await Task.Delay(250, token);
        }

        public async Task HitAsync(CancellationToken token = default)
        {
            _animator.Play(AnimationHashes.HitHash);
            await WaitUntilAnimationDoneAsync(token);
            _animator.Play(AnimationHashes.IdleHash);
        }

        public async Task DieAsync(CancellationToken token = default)
        {
            _animator.Play(AnimationHashes.DieHash);
            await WaitUntilAnimationDoneAsync(token);
        }

        private async Task WaitUntilAnimationDoneAsync(CancellationToken token = default)
        {
            await Task.Yield();
            await Task.Delay((int) (_animator.GetNextStateInfo().length * 1000), token);
            await Task.Yield();
        }

        public async Task ResetToIdleAsync(CancellationToken token = default)
        {
            await Task.Delay((int) (_animator.GetCurrentStateInfo().length * 1000 - 250), token);
            _animator.Play(AnimationHashes.IdleHash);
        }
    }
}