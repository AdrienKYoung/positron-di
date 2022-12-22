using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;
using Positron.Inject;

namespace Positron.Tests
{
    internal class ModuleTest
    {
        [Test] 
        public void createDependencyGraph_withEmptyModule_isEmpty()
        {
           var module = Module.Create(_ => { });
           module.Configure();
           Assert.IsEmpty(module.GetAllBindings());
        }

        [Test]
        public void resolveBinding_toType_success()
        {
            var module = Module.Create(module => { module.Bind<Cat>().To(typeof(Cat)).AsSingleton(); });
            module.Configure();
            Assert.AreSame(
                typeof(Cat), 
                module.GetBindingForType(typeof(Cat), null).Target
            );
        }

        [Test]
        public void resolveBinding_toSubtype_success()
        {
            var module = Module.Create(module => { module.Bind<Cat>().To(typeof(StandardIssueCat)).AsSingleton(); });
            module.Configure();
            Assert.AreSame(
                typeof(StandardIssueCat), 
                module.GetBindingForType(typeof(Cat), null).Target
            );
        }

        [Test]
        public void resolveBinding_noBinding_throws()
        {
            var module = Module.Create(module => { module.Bind<Cat>().To(typeof(StandardIssueCat)).AsSingleton(); });
            module.Configure();
            Assert.Throws<InjectionException>(() => module.GetBindingForType(typeof(Dog), null));
        }
    }
}
