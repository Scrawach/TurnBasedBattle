using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeBase.View.Processes.Abstract
{
    public interface IViewProcess
    {
        event Action<Func<CancellationToken, Task>> ProcessPrepared;
    }
}