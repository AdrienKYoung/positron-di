using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positron.Inject.Core
{
    internal class ProviderAdapter<T> : Provider<T>
    {
        private readonly IInternalProvider internalProvider;

        internal ProviderAdapter(IInternalProvider internalProvider)
        {
            this.internalProvider = internalProvider;
        }

        public T Get()
        {
            return (T) internalProvider.Get();
        }
    }

    internal interface IInternalProvider
    {
        object Get();
    }
}
