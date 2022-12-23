using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Positron.Inject.Core
{
    internal class ConstructorProvider : IInternalProvider
    {
        private readonly ConstructorInfo constructorInfo;
        private readonly Dictionary<ParameterInfo, IInternalProvider> providers;

        internal ConstructorProvider(
            ConstructorInfo constructorInfo, 
            Dictionary<ParameterInfo, IInternalProvider> providers)
        {
            this.constructorInfo = constructorInfo;
            this.providers = providers;
        }

        public object Get()
        {
            var parameters = constructorInfo.GetParameters().Select(p => providers[p].Get()).ToArray();
            return constructorInfo.Invoke(null, parameters);
        }
    }
}
