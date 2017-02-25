using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Powell.Data
{
    using FluentNHibernate.Conventions;

    /// <summary>
    /// The Finder implementation is limited by the underlying <see cref="IConventionFinder"/>,
    /// but should be functional enough for purposes of what I need and want to do.
    /// </summary>
    internal class ConventionFinderAdapter : IConventionFinderAdapter
    {
        private IConventionFinder Finder { get; }

        internal ConventionFinderAdapter(IConventionFinder finder)
        {
            Finder = finder;
        }

        private void FinderAction(Action<IConventionFinder> action)
        {
            action(Finder);
        }

        private TResult FinderFunc<TResult>(Func<IConventionFinder, TResult> func)
        {
            return func(Finder);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<IConvention> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public void Add(IConvention item) => FinderAction(f => f.Add(item));

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(IConvention item)
            => FinderFunc(f => f.Conventions.Any(t => item.GetType().IsAssignableFrom(t)));

        public void CopyTo(IConvention[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(IConvention item)
        {
            throw new NotImplementedException();
        }

        public int Count => FinderFunc(f => f.Conventions.Count());

        public bool IsReadOnly
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int IndexOf(IConvention item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, IConvention item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public IConvention this[int index]
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }
    }

    internal static class ConventionFinderExtensionmethods
    {
        internal static IList<IConvention> ToList(this IConventionFinder finder)
        {
            return new ConventionFinderAdapter(finder);
        }
    }
}
