using System.Collections.Generic;

namespace Powell
{
    public static class CollectionExtensionMethods
    {
        public static IList<T> AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            foreach (var item in items) list.Add(item);
            return list;
        }

        public static IList<T> AddRange<T>(this IList<T> list, params T[] items)
        {
            foreach (var item in items) list.Add(item);
            return list;
        }
    }
}
