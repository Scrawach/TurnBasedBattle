using System;
using System.Threading;
using System.Threading.Tasks;

namespace CodeBase.View.Processors.Abstract
{
    public interface IViewProcess
    {
        event Action<Func<CancellationToken, Task>> ProcessPrepared;
    }
}