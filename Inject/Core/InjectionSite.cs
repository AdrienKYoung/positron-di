using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;

namespace Positron.Inject.Core
{
    /// <summary>
    /// Top-level abstract class that defines where something is injected
    /// </summary>
    internal abstract class InjectionSite
    {
        internal abstract void Inject(InstanceResolver resolver, object target, InjectionContext context);
        internal abstract Type Dependency { get; }
    }

    internal class FieldInjectionSite : InjectionSite
    {
        private readonly FieldInfo fieldInfo;

        public FieldInjectionSite(FieldInfo fieldInfo)
        {
            this.fieldInfo = fieldInfo;
        }

        internal override Type Dependency => fieldInfo.FieldType;

        internal override void Inject(InstanceResolver resolver, object target, InjectionContext context)
        {
            fieldInfo.SetValue(target, resolver.ResolveInstance(fieldInfo.FieldType, context));
        }
    }

    internal class PropertyInjectionSite : InjectionSite
    {
        private readonly PropertyInfo propertyInfo;

        public PropertyInjectionSite(PropertyInfo propertyInfo)
        {
            this.propertyInfo = propertyInfo;
        }

        internal override Type Dependency => propertyInfo.PropertyType;

        internal override void Inject(InstanceResolver resolver, object target, InjectionContext context)
        {
            propertyInfo.SetValue(target, resolver.ResolveInstance(propertyInfo.PropertyType, context));
        }
    }
    
    internal class ConstructorInjectionSite : InjectionSite
    {
        internal readonly ConstructorInfo constructorInfo;

        public ConstructorInjectionSite(ConstructorInfo constructorInfo)
        {
            this.constructorInfo = constructorInfo;
        }

        internal override Type Dependency => null;

        internal override void Inject(InstanceResolver resolver, object target, InjectionContext context)
        {
            throw new NotImplementedException();
        }
    }

    internal class ParameterInjectionSite : InjectionSite
    {
        internal readonly ParameterInfo parameterInfo;
        internal readonly ConstructorInjectionSite constructor;

        public ParameterInjectionSite(ConstructorInjectionSite constructor, ParameterInfo parameterInfo)
        {
            this.constructor = constructor;
            this.parameterInfo = parameterInfo;
        }

        internal override void Inject(InstanceResolver resolver, object target, InjectionContext context)
        {
            throw new NotImplementedException();
        }

        internal override Type Dependency => parameterInfo.ParameterType;
    }
    
    internal class InjectionSiteCollection
    {
        internal List<InjectionSite> adHocInjectionSites { get; set; }
        internal ConstructorInjectionSite constructor;
    }
}
