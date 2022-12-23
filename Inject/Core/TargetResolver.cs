using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Positron.Inject.Core
{
    internal interface ITargetResolver
    {
        InjectionSiteCollection getInjectionSite(Type type, InjectionContext context);
    }

    internal class TargetResolver : ITargetResolver
    {
        private readonly Dictionary<Type, InjectionSiteCollection> injectionSiteCache = new();

        public InjectionSiteCollection getInjectionSite(Type type, InjectionContext context)
        {
            if (injectionSiteCache.ContainsKey(type))
                return injectionSiteCache[type];

            IEnumerable<InjectionSite> fieldSites = type.GetFields()
                .Select(field => new FieldInjectionSite(field));
            IEnumerable<InjectionSite> propertySites = type.GetProperties()
                .Select(prop => new PropertyInjectionSite(prop));

<<<<<<< HEAD
            var explicitConstructorInjectionSites = type.GetConstructors()
                .Where(c => c.HasCustomAttribute(typeof(InjectAttribute)))
                .ToList();

            var implicitConstructorInjectionSites = type.GetConstructors()
=======
            var constructors = type.GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var explicitConstructorInjectionSites = constructors
                .Where(c => c.HasCustomAttribute(typeof(InjectAttribute)))
                .ToList();

            var implicitConstructorInjectionSites = constructors
>>>>>>> 2768a82 (basic graph resolution of dependencies through constructors)
                .Where(c => !c.HasCustomAttribute(typeof(InjectAttribute)))
                .ToList();

            //TODO - may want to defer constructor resolution and instead try to find one that works
            if (explicitConstructorInjectionSites.Count > 1)
                throw new InjectionException($"Type {type.FullName} has multiple explicitly injected constructors");

            var constructor = explicitConstructorInjectionSites.Count == 1
                ? explicitConstructorInjectionSites.First()
                : implicitConstructorInjectionSites
                    .OrderByDescending(c => c.GetParameters().Length)
                    .First();

            var result = new InjectionSiteCollection()
            {
                adHocInjectionSites = fieldSites.Concat(propertySites).ToList(),
                constructor = new ConstructorInjectionSite(constructor)
            };

            injectionSiteCache.Add(type, result);
            return result;
        }
    }
}
