using System;

namespace CodeBase.View.Processes.Abstract
{
    public class ViewProcess : IViewProcess
    {
        public event Action<ViewRequest> ProcessPrepared;

        protected void Process(ViewRequestDelegate request, bool isBlocking = true) =>
            ProcessPrepared?.Invoke(new ViewRequest(request, isBlocking));
    }
}