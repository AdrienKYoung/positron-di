using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Positron.Inject;
using Positron.Inject.Core;

namespace Positron.Tests
{
    internal class DependencyGraphTests
    {

        private DependencyGraphFactory factory = new DependencyGraphFactory(new TargetResolver());

        [Test]
        public void createDependencyGraph_singleConstructor_success()
        {
            var module = Module.Create(module => { 
                module.Bind<Cat>().To(typeof(StandardIssueCat)).AsSingleton();
                module.Bind<PetOwner>().To(typeof(PetOwner)).AsSingleton();
            });
            module.Configure();
            var graph = factory.createDependencyGraph(module);
            Assert.IsNotEmpty(graph.Types);
        }
    }
}
