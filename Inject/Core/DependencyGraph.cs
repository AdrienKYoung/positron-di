using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Positron.Inject.Core
{
    internal class DependencyGraph
    {
        internal Dictionary<Type, TypeNode> Types { get; private init; }

        internal DependencyGraph(Dictionary<Type, TypeNode> types)
        {
            this.Types = types;
        }
        internal DependencyGraph(IEnumerable<TypeNode> dependencies)
        {
            this.Types = dependencies.ToDictionary(d => d.TargetType);
        }
    }

    internal class TypeNode
    {
        internal Type TargetType { get; set; }
        internal Binding Binding { get; set; }
        internal InjectionSiteCollection InjectionSites { get; set; }
        internal Dictionary<InjectionSite, TypeNode> Dependencies { get; set; } //TODO: Need to merge the injection sites with type nodes
    }

    internal class DependencyGraphFactory
    {
        private readonly ITargetResolver targetResolver;

        internal DependencyGraphFactory(ITargetResolver targetResolver)
        {
            this.targetResolver = targetResolver;
        }

        internal DependencyGraph createDependencyGraph(Module module)
        {
            Dictionary<Type, TypeNode> types = new();
            
            foreach (var binding in module.GetAllBindings())
            {
<<<<<<< HEAD
                types.Add(binding.Target, resolveTypeNode(binding.Target, module, types));
=======
                if (!types.ContainsKey(binding.Target))
                {
                    resolveTypeNode(binding.Target, module, types);
                }
>>>>>>> 2768a82 (basic graph resolution of dependencies through constructors)
            }

            return new DependencyGraph(types);
        }

        private TypeNode resolveTypeNode(Type t, Module module, Dictionary<Type, TypeNode> store)
        {
            if (store.ContainsKey(t)) return store[t];

            var injectionSites = targetResolver.getInjectionSite(t, null);
            var dependencies = injectionSites.adHocInjectionSites
                .ToDictionary(
                    site => site, 
                    site => resolveTypeNode(site.Dependency, module, store));

            var constructorParams = injectionSites.constructor.constructorInfo.GetParameters();
            foreach (var c in constructorParams)
            {
                dependencies.Add(
                    new ParameterInjectionSite(injectionSites.constructor, c), 
                    resolveTypeNode(c.ParameterType, module, store));
            }

            var node = new TypeNode()
            {
                TargetType = t,
                Binding = module.GetBindingForType(t, null),
                InjectionSites = injectionSites,
                Dependencies = dependencies
            };

            store.Add(t, node);
            return node;
        }
    }
}
