using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positron.Inject
{
    public static class Positron
    {
    }

    public sealed class InjectionException : Exception
    {
        public InjectionException(String cause) : base(cause) { }
    }
}
