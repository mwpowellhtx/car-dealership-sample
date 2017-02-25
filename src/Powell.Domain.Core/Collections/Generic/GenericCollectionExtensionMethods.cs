using System;
using System.Collections.Generic;

namespace Powell.Collections.Generic
{
    internal static class GenericCollectionExtensionMethods
    {
        /// <summary>
        /// Performs the <paramref name="action"/> over the range of <paramref name="values"/>.
        /// Similar to the <see cref="List{T}"/> version except for <see cref="IEnumerable{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="values"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static IEnumerable<T> ForEach<T>(this IEnumerable<T> values, Action<T> action)
        {
            // ReSharper disable once PossibleMultipleEnumeration
            foreach (var value in values) action(value);

            // ReSharper disable once PossibleMultipleEnumeration
            return values;
        }
    }
}
