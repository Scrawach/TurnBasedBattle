using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace CodeBase.View.UI.Abstract
{
    public abstract class AsyncViewValue : MonoBehaviour
    {
        public Task Current { get; private set; }

        public async Task UpdateValue(int current, int total, float timeInSeconds, CancellationToken token = default)
        {
            Current = Updating(current, total, timeInSeconds, token);
            await Current;
        }

        public void Show() =>
            gameObject.SetActive(true);

        public void Hide() =>
            gameObject.SetActive(false);

        protected abstract Task Updating(int current, int total, float timeInSeconds, CancellationToken token = default);
    }
}