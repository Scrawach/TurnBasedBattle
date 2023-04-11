using System.Threading;
using System.Threading.Tasks;

namespace CodeBase.View.Processes.Abstract
{
    public delegate Task ViewRequestDelegate(CancellationToken token);

    public class ViewRequest
    {
        public readonly ViewRequestDelegate Request;
        public readonly bool IsBlocking;

        public ViewRequest(ViewRequestDelegate request, bool isBlocking = true)
        {
            Request = request;
            IsBlocking = isBlocking;
        }

        public Task Invoke(CancellationToken token) =>
            Request.Invoke(token);
    }
}