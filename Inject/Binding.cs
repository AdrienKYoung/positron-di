using System;
using System.Collections.Generic;
using System.Text;

namespace Positron.Inject
{
    public interface Binding
    {
        public Type Target { get; }
    }
}
