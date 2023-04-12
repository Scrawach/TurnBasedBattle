using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.View.Characters.Components
{
    public class AsyncRotator : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        public async Task RotateAsync(Vector3 to, CancellationToken token = default)
        {
            var selfTransform = transform;
            var direction = to - selfTransform.position;
            var startRotation = selfTransform.rotation;
            var targetRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            var progress = 0f;

            while (progress < 1f)
            {
                progress += Time.deltaTime * _speed;
                transform.rotation = Quaternion.Slerp(startRotation, targetRotation, progress);
                await Task.Yield();
                token.ThrowIfCancellationRequested();
            }
        }
    }
}