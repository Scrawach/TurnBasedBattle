using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.UI.Abstract;
using UnityEngine;
using UnityEngine.UI;

namespace CodeBase.View.UI
{
    public class AsyncSlider : AsyncViewValue
    {
        [SerializeField] private Slider _bar;
        
        protected override async Task Updating(int value, int total, float timeInSeconds, CancellationToken token = default)
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
    }
}