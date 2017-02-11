using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Powell.Collections.Generic
{
    /// <summary>
    /// Added delegate specification.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    public delegate void AddedDelegate<in T>(T item);

    /// <summary>
    /// Removed delegate specification.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="item"></param>
    public delegate void RemovedDelegate<in T>(T item);

    /// <summary>
    /// Concrete implementation of the <see cref="IBidirectionalList{T}"/>. Use
    /// <see cref="BidirectionalListExtensionMethods.ToBidirectionalList{T}"/>
    /// in order to expose into the model.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BidirectionalList<T> : IBidirectionalList<T>
    {
        public AddedDelegate<T> OnAdded { get; }

        public RemovedDelegate<T> OnRemoved { get; }

        private readonly IList<T> _items;

        internal BidirectionalList(AddedDelegate<T> onAdded = null, RemovedDelegate<T> onRemoved = null)
            : this(new List<T>(), onAdded, onRemoved)
        {
        }

        internal BidirectionalList(IList<T> items, AddedDelegate<T> onAdded = null, RemovedDelegate<T> onRemoved = null)
        {
            _items = items;
            OnAdded = onAdded ?? delegate { };
            OnRemoved = onRemoved ?? delegate { };
        }

        private void ListAction(Action<IList<T>> action) => action(_items);

        private TResult ListFunc<TResult>(Func<IList<T>, TResult> func) => func(_items);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public IEnumerator<T> GetEnumerator() => ListFunc(l => l.GetEnumerator());

        public void Add(T item) => ListAction(l =>
        {
            l.Add(item);
            OnAdded(item);
        });

        public void Clear() => ListAction(l =>
        {
            var items = _items.ToArray();
            l.Clear();
            foreach (var item in items) OnRemoved(item);
        });

        public bool Contains(T item) => ListFunc(l => l.Contains(item));

        public void CopyTo(T[] array, int arrayIndex) => ListAction(l => l.CopyTo(array, arrayIndex));

        public bool Remove(T item) => ListFunc(l =>
        {
            var removed = l.Remove(item);
            if (removed) OnRemoved(item);
            return removed;
        });

        public int Count => ListFunc(l => l.Count);

        public bool IsReadOnly => ListFunc(l => l.IsReadOnly);

        public int IndexOf(T item) => ListFunc(l => l.IndexOf(item));

        public void Insert(int index, T item) => ListAction(l =>
        {
            l.Insert(index, item);
            OnAdded(item);
        });

        public void RemoveAt(int index) => ListAction(l =>
        {
            var item = l[index];
            l.RemoveAt(index);
            OnRemoved(item);
        });

        public T this[int index]
        {
            get { return ListFunc(l => l[index]); }
            set
            {
                ListAction(l =>
                {
                    var old = l[index];
                    l[index] = value;
                    if (old != null) OnRemoved(old);
                    if (value != null) OnAdded(value);
                });
            }
        }
    }
}
