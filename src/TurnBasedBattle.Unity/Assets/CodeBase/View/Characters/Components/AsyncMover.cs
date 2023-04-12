using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.View.Characters.Components
{
    public class AsyncMover : MonoBehaviour
    {
        [SerializeField] private float _speed;
        
        public async Task MoveAsync(Vector3 at, float stoppingDistance, CancellationToken token = default)
        {
            var startPosition = transform.position;
            var distance = Vector3.Distance(startPosition, at);
            var countOfSteps = distance / _speed;
            var progress = 0f;
            
            while (progress < 1f)
            {
                if (Vector3.Distance(transform.position, at) <= stoppingDistance) 
                    break;

                progress += Time.deltaTime / countOfSteps;
                transform.position = Vector3.Lerp(startPosition, at, progress);
                
                await Task.Yield();
                token.ThrowIfCancellationRequested();
            }
        }
    }
}