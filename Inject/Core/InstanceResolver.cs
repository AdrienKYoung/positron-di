using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positron.Inject.Core
{
    internal interface IInstanceResolver
    {
        object ResolveInstance(Type type, InjectionContext context);
        object ResolveProvider(Type type, InjectionContext context);
    }

    internal class InstanceResolver : IInstanceResolver
    {
        public object ResolveInstance(Type type, InjectionContext context)
        {
            throw new NotImplementedException();
        }

        public object ResolveProvider(Type type, InjectionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
