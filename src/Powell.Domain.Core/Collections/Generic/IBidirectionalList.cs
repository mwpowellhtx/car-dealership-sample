using System.Collections.Generic;

namespace Powell.Collections.Generic
{
    /// <summary>
    /// Represents the bidirectional version of the <see cref="IList{T}"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBidirectionalList<T> : IList<T>
    {
    }

    public static class BidirectionalListExtensionMethods
    {
        /// <summary>
        /// Returns the <paramref name="items"/> wrapped in the <see cref="BidirectionalList{T}"/>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="items"></param>
        /// <param name="onAdded"></param>
        /// <param name="onRemoved"></param>
        /// <returns></returns>
        public static IBidirectionalList<T> ToBidirectionalList<T>(this IList<T> items,
            AddedDelegate<T> onAdded = null, RemovedDelegate<T> onRemoved = null)
        {
            return new BidirectionalList<T>(items, onAdded, onRemoved);
        }
    }
}
