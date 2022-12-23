using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positron.Inject.Core
{
    internal class InjectionSolver : Injector
    {
        private readonly Module module;
        private readonly InjectionContext injectionContext;
        private readonly IInstanceResolver instanceResolver;
        private readonly ITargetResolver targetResolver;

        internal InjectionSolver(Module module, InjectionContext injectionContext)
        {
            this.module = module;
            this.injectionContext = injectionContext;
            this.instanceResolver = new InstanceResolver();
            this.targetResolver = new TargetResolver();
        }

        public T Inject<T>(T target)
        {
            throw new NotImplementedException();
        }

        public T Create<T>(Type type)
        {
            throw new NotImplementedException();
        }

        public Provider<T> GetProvider<T>(Type type)
        {
            throw new NotImplementedException();
        }
    }
}
