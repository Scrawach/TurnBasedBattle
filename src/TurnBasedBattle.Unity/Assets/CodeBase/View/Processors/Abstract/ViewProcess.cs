using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeBase.View.Processors.Abstract
{
    public class ViewProcess : IViewProcess
    {
        public event Action<Func<CancellationToken, Task>> ProcessPrepared;

        protected void Process(Func<CancellationToken, Task> task) =>
            ProcessPrepared?.Invoke(task);
    }
}