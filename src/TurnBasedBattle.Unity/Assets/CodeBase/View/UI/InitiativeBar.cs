using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.View.UI
{
    public class InitiativeBar : MonoBehaviour
    {
        [SerializeField] private Slider _bar;
        
        public Task Current { get; private set; }

        public async Task Show(int value, int total, float timeInSeconds, CancellationToken token = default)
        {
            Current = Updating(value, total, timeInSeconds, token);
            await Current;
        }

        private async Task Updating(int value, int total, float timeInSeconds, CancellationToken token = default)
        {
            var startValue = _bar.value;
            var desiredValue = (float) value / total;
            var progress = 0f;
            
            while (progress < timeInSeconds)
            {
                progress += Time.deltaTime;
                _bar.value = Mathf.Lerp(startValue, desiredValue, progress / timeInSeconds);
                await Task.Yield();
                token.ThrowIfCancellationRequested();
            }
        }

        public void Hide() =>
            _bar.gameObject.SetActive(false);
    }
}