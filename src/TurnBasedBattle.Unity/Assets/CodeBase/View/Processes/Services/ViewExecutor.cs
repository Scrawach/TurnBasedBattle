using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Processes.Abstract;
using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.EventBus.Abstract;

namespace CodeBase.View.Processes.Services
{
    public class ViewExecutor : IViewExecutor, IDisposable
    {
        private readonly IEventBus<ICommand> _bus;
        private readonly IViewProcess[] _processes;
        
        private readonly Queue<Func<CancellationToken, Task>> _queueOfProcesses;
        private readonly CancellationTokenSource _source = new CancellationTokenSource();

        public ViewExecutor(params IViewProcess[] processes)
        {
            _processes = processes;
            _queueOfProcesses = new Queue<Func<CancellationToken, Task>>();
        }

        public void Subscribe()
        {
            foreach (var process in _processes)
            {
                process.ProcessPrepared += AddProcessInQueue;
            }
        }

        public void Unsubscribe()
        {
            foreach (var process in _processes)
            {
                process.ProcessPrepared -= AddProcessInQueue;
            }
        }

        private void AddProcessInQueue(Func<CancellationToken, Task> process) =>
            _queueOfProcesses.Enqueue(process);

        public async Task Execute()
        {
            while (_queueOfProcesses.Count > 0)
            {
                var command = _queueOfProcesses.Dequeue();
                await command.Invoke(_source.Token);
            }
        }

        public void Dispose()
        {
            _source.Cancel();
            _source?.Dispose();
        }
    }
}