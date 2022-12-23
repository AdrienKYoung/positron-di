using System;
using System.Collections.Generic;
using System.Linq;
using Positron.Inject;

namespace Positron.Tests
{
    internal abstract class Mammal { }
    internal class Cat : Mammal { }
    internal class StandardIssueCat : Cat { }
    internal class Dog : Mammal { }
    internal class PetOwner {
        [Inject] PetOwner(Mammal mammal) { }
    }
}
