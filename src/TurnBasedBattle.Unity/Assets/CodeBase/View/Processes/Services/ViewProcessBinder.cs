using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using CodeBase.View.Attributes;
using CodeBase.View.Processes.Abstract;
using TurnBasedBattle.Model.Commands.Abstract;
using TurnBasedBattle.Model.EventBus.Abstract;

namespace CodeBase.View.Processes.Services
{
    public class ViewProcessBinder
    {
        private readonly object[] _dependencies;
        private readonly IEventBus<ICommand> _eventBus;
        private readonly List<IViewProcess> _binded;
        private ViewExecutor _executor;

        public ViewProcessBinder(IEventBus<ICommand> eventBus, params object[] dependencies)
        {
            _eventBus = eventBus;
            _dependencies = dependencies;
            _binded = new List<IViewProcess>();
        }

        public ViewExecutor Executor()
        {
            var constructor = typeof(ViewExecutor).GetConstructors().First();
            _executor = constructor.Invoke(new object[] { _binded.ToArray() }) as ViewExecutor;
            _executor.Subscribe();
            return _executor;
        }

        public void BindAll()
        {
            foreach (var (attribute, type) in FindTypesWithAttribute<ViewProcessAttribute>())
            {
                var instance = CreateInstance(type, _dependencies);
                Subscribe(_eventBus, withType: attribute.Type, instance);
                _binded.Add(instance as IViewProcess);
            }
        }

        public void UnbindAll()
        {
            foreach (var (attribute, type) in FindTypesWithAttribute<ViewProcessAttribute>())
            {
                var instance = CreateInstance(type, _dependencies);
                Unsubscribe(_eventBus, withType: attribute.Type, instance);
            }
            
            _executor.Unsubscribe();
            _binded.Clear();
        }

        private static object CreateInstance(Type type, IReadOnlyCollection<object> dependencies)
        {
            var constructor = type.GetConstructors().First();
            var parameters = constructor.GetParameters();
            var resolved = parameters
                .SelectMany(parameter => dependencies.Where(obj => parameter.ParameterType.IsInstanceOfType(obj)))
                .ToList();
            var instance = constructor.Invoke(resolved.ToArray());
            return instance;
        }

        private static void Subscribe(IEventBus<ICommand> bus, Type withType, object arg)
        {
            var subscribeMethod = bus.GetType().GetMethod(nameof(IEventBus<ICommand>.Subscribe));
            var genericMethodInfo = subscribeMethod?.MakeGenericMethod(withType);
            genericMethodInfo?.Invoke(bus, new []{ arg });
        }

        private static void Unsubscribe(IEventBus<ICommand> bus, Type withType, object arg)
        {
            var subscribeMethod = bus.GetType().GetMethod(nameof(IEventBus<ICommand>.Unsubscribe));
            var genericMethodInfo = subscribeMethod?.MakeGenericMethod(withType);
            genericMethodInfo?.Invoke(bus, new []{ arg });
        }

        private static IEnumerable<(TAttribute attribute, Type type)> FindTypesWithAttribute<TAttribute>()
        {
            foreach (var type in Assembly.GetAssembly(typeof(ViewProcessBinder)).GetTypes())
            {
                var attributes = type.GetCustomAttributes(true);
                
                foreach (var attribute in attributes)
                {
                    if (attribute is TAttribute casted)
                        yield return (casted, type);
                }
            }
        }
    }
}