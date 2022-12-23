using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Positron.Inject.Core;

namespace Positron.Inject
{
    public abstract class Module
    {
        private readonly Dictionary<Type, Binding> bindings = new();
        private readonly List<Module> children = new();

        public abstract void Configure();

        public OngoingBinding<T> Bind<T>()
        {
            return new OngoingBinding<T>
            {
                Source = this
            };
        }

        public void Install(Module module)
        {
            children.Add(module);
        }

        internal void CompleteBinding<T>(OngoingBinding<T> ongoingBinding)
        {
            this.bindings.Add(typeof(T), ongoingBinding.Binding);
        }

        internal Binding GetBindingForType(Type type, InjectionContext context)
        {
            if (bindings.ContainsKey(type)) return bindings[type];

            return children
                .Select(c => c.GetBindingForType(type, context))
                .NonNull()
                .FirstOrThrow(new InjectionException($"No binding for type {type.FullName}"));
        }

        internal IEnumerable<Binding> GetAllBindings()
        {
            return bindings.Values.Union(children.SelectMany(child => child.GetAllBindings()));
        }

        public static Module Create(Action<Module> binderAction)
        {
            return new LambdaModule(binderAction);
        }
    }

    public class OngoingBinding<T>
    {
        internal Binding Binding { get; set; }
        internal Module Source { get; set; }
        internal ScopeType ScopeType { get; set; }

        public OngoingBinding<T> To(Type resolution)
        {
            Binding = new TypedBinding(typeof(T), resolution);
            return this;
        }

        public OngoingBinding<T> To(T instance)
        {
            Binding = new InstanceBinding(typeof(T), instance);
            return this;
        }

        public void AsSingleton()
        {
            ScopeType = ScopeType.Singleton;
            Source.CompleteBinding(this);
        }
    }

    internal class LambdaModule : Module
    {
        private readonly Action<Module> binderAction;

        internal LambdaModule(Action<Module> binderAction)
        {
            this.binderAction = binderAction;
        }

        public override void Configure()
        {
            binderAction.Invoke(this);
        }
    }
}
