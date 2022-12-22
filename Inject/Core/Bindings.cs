using System;
using System.Collections.Generic;
using System.Text;

namespace Positron.Inject.Core
{
    public record TypedBinding(Type Target, Type Resolution) : Binding { }

    public record InstanceBinding(Type Target, object Instance) : Binding { }

    public record ProviderBinding<T>(Type Target, Provider<T> Provider) : Binding { }
}
