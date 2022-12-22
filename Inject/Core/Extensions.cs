using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Positron.Inject.Core
{
    internal static class Extensions
    {
        internal static IEnumerable<T> NonNull<T>(this IEnumerable<T> stream) => stream.Where(v => v != null);

        public static bool HasCustomAttribute(this MemberInfo element, Type attribute)
        {
            return Attribute
                .GetCustomAttributes(element, true)
                .Any(a => a.GetType() == attribute);
        }

        public static bool HasCustomAttribute(this ParameterInfo element, Type attribute)
        {
            return Attribute
                .GetCustomAttributes(element, true)
                .Any(a => a.GetType() == attribute);
        }

        public static T FirstOrThrow<T>(this IEnumerable<T> sequence, Exception exception)
        {
            var result = sequence.FirstOrDefault();
            if (result != null) return result;
            else throw exception;
        }

        public static V Upsert<K,V>(Dictionary<K,V> collection, K key, Func<V> creationFunc)
        {
            if(collection.ContainsKey(key))
            {
                return collection[key];
            }
            else
            {
                var value = creationFunc();
                collection[key] = value;
                return value;
            }
        }
    }
}
