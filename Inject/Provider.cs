using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Positron.Inject
{
    public interface Provider<out T>
    {
        T Get();
    }
}
