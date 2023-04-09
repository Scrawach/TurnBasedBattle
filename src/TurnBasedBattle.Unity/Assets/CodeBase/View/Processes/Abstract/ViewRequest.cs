using System.Threading;
using System.Threading.Tasks;

namespace CodeBase.View.Processes.Abstract;

public delegate Task ViewRequest(CancellationToken token);