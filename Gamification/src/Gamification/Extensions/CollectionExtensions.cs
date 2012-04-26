using System.Collections.Generic;
using System.Linq;
using LinqSpecs;

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

        public static IQueryable<T> BySpec<T>(this IQueryable<T> query, Specification<T> spec)
        {
            return query.Where(spec.IsSatisfiedBy());
        }

        public static T FirstBySpec<T>(this IQueryable<T> query, Specification<T> spec)
        {
            return query.FirstOrDefault(spec.IsSatisfiedBy());
        }
    }
}
