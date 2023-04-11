using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CodeBase.View.Processes.Abstract;
using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.EventBus.Abstract;
using UnityEngine;

namespace CodeBase.View.Processes.Services
{
    public class ViewExecutor : IViewExecutor, IDisposable
    {
        private readonly IEventBus<ICommand> _bus;
        private readonly IViewProcess[] _processes;
        
        private readonly Queue<ViewRequest> _queueOfProcesses;
        private readonly CancellationTokenSource _source = new CancellationTokenSource();

        public ViewExecutor(params IViewProcess[] processes)
        {
            _processes = processes;
            _queueOfProcesses = new Queue<ViewRequest>();
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

        private void AddProcessInQueue(ViewRequest process) =>
            _queueOfProcesses.Enqueue(process);

        public async Task Execute()
        {
            var processes = new List<Task>();
            
            while (_queueOfProcesses.Count > 0)
            {
                var command = _queueOfProcesses.Dequeue();
                var task = command.Invoke(_source.Token);
                processes.Add(task);
                
                if (command.IsBlocking)
                    await task;
            }

            await Task.WhenAll(processes);
        }

        public void Dispose()
        {
            _source.Cancel();
            _source?.Dispose();
        }
    }
}