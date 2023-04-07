using System.Threading;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.View.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _bar;
        [SerializeField] private TextMeshProUGUI _healthValue;

        public async Task Show(int value, int total, float time, CancellationToken token = default) =>
            await Updating(value, total, time, token);
        
        private async Task Updating(int value, int total, float timeInSeconds, CancellationToken token = default)
        {
            var startValue = _bar.value;
            var desiredValue = (float) value / total;
            var progress = 0f;

            var currentValue = (int) (startValue * total);

            while (progress < timeInSeconds)
            {
                progress += Time.deltaTime;
                _bar.value = Mathf.Lerp(startValue, desiredValue, progress / timeInSeconds);

                var step = (int) (_bar.value * total);
                if (step != currentValue)
                {
                    currentValue = step;
                    _healthValue.text = $"{currentValue} / {total}";
                }

                await Task.Yield();
                token.ThrowIfCancellationRequested();
            }
        }
    }
}