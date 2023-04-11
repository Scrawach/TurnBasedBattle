using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.UI.Abstract;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.View.UI
{
    public class StatBar : AsyncViewValue
    {
        [SerializeField] private Slider _bar;
        [SerializeField] private TextMeshProUGUI _label;

        protected override async Task Updating(int value, int total, float timeInSeconds, CancellationToken token = default)
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
                    _label.text = $"{currentValue} / {total}";
                }

                await Task.Yield();
                token.ThrowIfCancellationRequested();
            }
        }
    }
}