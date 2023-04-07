using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.View.UI
{
    public class InitiativeBar : MonoBehaviour
    {
        [SerializeField] private Slider _bar;

        public async Task Show(int value, int total, float timeInSeconds, CancellationToken token = default) =>
            await Updating(value, total, timeInSeconds, token);

        private async Task Updating(int value, int total, float timeInSeconds, CancellationToken token = default)
        {
            var startValue = _bar.value;
            var desiredValue = (float) value / total;
            var progress = 0f;
            
            while (progress < timeInSeconds)
            {
                progress += Time.deltaTime;
                _bar.value = Mathf.Lerp(startValue, desiredValue, progress / timeInSeconds);
                token.ThrowIfCancellationRequested();
                await Task.Yield();
            }
        }
    }
}