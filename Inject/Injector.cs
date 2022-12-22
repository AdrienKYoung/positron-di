using System;
using System.Collections.Generic;
using System.Text;

namespace Positron.Inject
{
    public interface Injector
    {
        public T Inject<T>(T target);
        public T Create<T>(Type type);
        public Provider<T> GetProvider<T>(Type type);
    }
}
