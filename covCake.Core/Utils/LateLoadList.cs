using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace covCake
{

    /// <summary>
    /// An IList implementation that flexes IQueryable's delayed loading
    /// </summary>
    /// <typeparam name="T">IList of T</typeparam>
    public class LateLoadList<T> : IList<T>
    {

        private IQueryable<T> _query;
        private IList<T> _innerList;

        public IList<T> InnerList
        {
            get
            {
                if (_innerList == null)
                    _innerList = _query.ToList();
                return _innerList;
            }
        }

        public LateLoadList()
        {
            _innerList = new List<T>();
        }

        public LateLoadList(IQueryable<T> query)
        {
            this._query = query;
        }

      

        public int IndexOf(T item)
        {
            return InnerList.IndexOf(item);
        }

        public void Insert(int index, T item)
        {
            InnerList.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            InnerList.RemoveAt(index);
        }

        public T this[int index]
        {
            get { return InnerList[index]; }
            set { InnerList[index] = value; }
        }

        //public void Add(IQueryable<T> query)
        //{
        //    _query.Concat<IQueryable<T>>(query);
        //}

        public void Add(T item)
        {
            _innerList = _innerList ?? new List<T>();
            InnerList.Add(item);
        }

        public void Add(object ob)
        {
            throw new NotImplementedException("This is for serialization");

        }

        public void Clear()
        {
            if (_innerList != null)
                InnerList.Clear();
        }

        public bool Contains(T item)
        {
            return InnerList.Contains(item);
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            InnerList.CopyTo(array, arrayIndex);
        }

        public bool Remove(T item)
        {
            return InnerList.Remove(item);
        }

        public int Count
        {
            get { return InnerList.Count; }
        }

        public bool IsReadOnly
        {
            get { return InnerList.IsReadOnly; }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return InnerList.GetEnumerator();
        }

        public IEnumerator GetEnumerator()
        {
            return ((IEnumerable)InnerList).GetEnumerator();
        }

        
    }

}
