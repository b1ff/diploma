using System;
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

        public static TValue SafeGet<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey key, TValue value = default(TValue))
        {
            return dictionary.ContainsKey(key) ? dictionary[key] : value;
        }

        public static void Add<TElement>(this ICollection<TElement> elements, IEnumerable<TElement> elementsToAdd)
        {
            foreach (var element in elementsToAdd)
            {
                elements.Add(element);
            }
        }

        public static IEnumerable<TElem> GetDiff<TElem>(this IEnumerable<TElem> elems1, IEnumerable<TElem> elems2)
        {
            if (elems1 == null)
                return elems2;
            if (elems2 == null)
                return elems1;

            return elems1.Where(x => !elems2.Contains(x)).Union(elems2.Where(x => !elems1.Contains(x)));
        }

        public static string JoinToString<TElem>(this IEnumerable<TElem> collection, Func<TElem, string> fieldToJoin)
        {
            return string.Join(", ", collection.Select(fieldToJoin));
        }
    }
}
