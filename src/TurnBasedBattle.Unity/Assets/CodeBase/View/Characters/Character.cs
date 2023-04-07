using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.UI;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CodeBase.View.Characters
{
    public class Character : MonoBehaviour
    {
        [SerializeField] private CharacterAnimator _animator;
        [SerializeField] private float _moveSpeed;
        [SerializeField] private float _rotationSpeed;

        [SerializeField] private HealthBar _healthBar;
        [SerializeField] private InitiativeBar _initiativeBar;
        
        public async Task SetHealthBarAsync(int value, int total, float timeInSeconds, CancellationToken token = default) =>
            await _healthBar.Show(value, total, timeInSeconds, token);

        public async Task SetInitiativeBarAsync(int resultInitiative, int total, float timeInSeconds, CancellationToken token = default) =>
            await _initiativeBar.Show(resultInitiative, total, timeInSeconds, token);

        public async Task MoveAsync(Vector3 at, float stoppingDistance, CancellationToken token = default)
        {
            var startPosition = transform.position;
            var distance = Vector3.Distance(startPosition, at);
            var countOfSteps = distance / _moveSpeed;
            var progress = 0f;
            
            _animator.Play(AnimationHashes.RunHash);
            while (progress < 1f)
            {
                if (Vector3.Distance(transform.position, at) <= stoppingDistance) 
                    break;

                progress += Time.deltaTime / countOfSteps;
                transform.position = Vector3.Lerp(startPosition, at, progress);
                token.ThrowIfCancellationRequested();
                await Task.Yield();
            }
            _animator.Play(AnimationHashes.IdleHash);
            await Task.Yield();
        }

        public async Task RotateAsync(Vector3 to, CancellationToken token = default)
        {
            var selfTransform = transform;
            var direction = to - selfTransform.position;
            var startRotation = selfTransform.rotation;
            var targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            var progress = 0f;

            while (progress < 1f)
            {
                progress += Time.deltaTime * _rotationSpeed;
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);
                token.ThrowIfCancellationRequested();
                await Task.Yield();
            }
        }

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