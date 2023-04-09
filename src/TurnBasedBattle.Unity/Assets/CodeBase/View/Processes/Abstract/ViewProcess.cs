using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeBase.View.Processes.Abstract
{
    public class ViewProcess : IViewProcess
    {
        public event Action<ViewRequest> ProcessPrepared;

        protected void Process(ViewRequest request) =>
            ProcessPrepared?.Invoke(request);
    }
}