using System.Collections.Generic;
using System.Linq;

namespace Gamification.Core.Extensions
{
    public static class CollectionExtensions
    {
        public static bool IsEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || !collection.Any();
        }

        public static bool IsNotEmpty<T>(this IEnumerable<T> collection)
        {
            return collection != null && collection.Any();
        }
    }
}
