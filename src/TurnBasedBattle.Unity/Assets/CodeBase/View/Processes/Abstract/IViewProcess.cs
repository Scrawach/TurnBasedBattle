using System;

namespace CodeBase.View.Processes.Abstract
{
    public interface IViewProcess
    {
        event Action<ViewRequest> ProcessPrepared;
    }
}