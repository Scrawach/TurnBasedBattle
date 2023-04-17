using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.EventBus.Abstract;

namespace CodeBase.View.Processes.Services
{
    public class ViewExecutorBuilder
    {
        private readonly IDictionary<Type, object> _instances;

        public ViewExecutorBuilder() =>
            _instances = new Dictionary<Type, object>();

        public ViewExecutorBuilder Bind<TInterface, TImplementation>()
            where TImplementation : class, TInterface
        {
            var (constructor, arguments) = ConstructorWithArgumentsFrom(typeof(TImplementation));
            _instances[typeof(TInterface)] = constructor.Invoke(arguments);
            return this;
        }

        public ViewExecutor Build(IEventBus<ICommand> eventBus)
        {
            var binder = new ViewProcessBinder(eventBus, _instances.Values.ToArray());
            binder.BindAll();
            return binder.Executor();
        }

        private (ConstructorInfo constructor, object[] arguments) ConstructorWithArgumentsFrom(Type type)
        {
            var (constructor, parameters) = AvailableConstructorsWithArgumentsFrom(type).First();
            return 
            (
                constructor, 
                parameters.Select(info => _instances[info.ParameterType]).ToArray()
            );
        }

        private IEnumerable<(ConstructorInfo, ParameterInfo[])> AvailableConstructorsWithArgumentsFrom(Type type) =>
            from constructor in type.GetConstructors()
            let parameters = constructor.GetParameters()
            where parameters.All(parameter => _instances.ContainsKey(parameter.ParameterType))
            select (constructor, parameters);
    }
}