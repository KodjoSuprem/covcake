using System;
using System.Collections.Generic;
using System.Linq;
using covCake.DataAccess;

namespace covCake
{

    public class PagedList<T> : List<T>, IList<T>
    {

        public int TotalPages
        {
            get;
            set;
        }

        public int TotalCount
        {
            get;
            set;
        }

        public int PageIndex
        {
            get;
            set;
        }

        public int PageSize
        {
            get;
            set;
        }

        public bool IsPreviousPage
        {
            get
            {
                return (PageIndex > 0);
            }
        }

        public bool IsNextPage
        {
            get
            {
                return (PageIndex * PageSize) <= TotalCount;
            }
        }

        public PagedList(IQueryable<T> source, int index, int pageSize)
        {
            source = (source != null ) ? source : new List<T>().AsQueryable();
            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = index;

            this.AddRange(source.Skip(index * pageSize).Take(pageSize).ToList());
        }

        /// <summary>
        /// Useless
        /// </summary>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <param name="pageSize"></param>
        [Obsolete("sert a rien")]
        public PagedList(List<T> source, int index, int pageSize)
        {

            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;


            this.PageSize = pageSize;
            this.PageIndex = index;
       
            this.AddRange(source.Skip(index * pageSize).Take(pageSize).ToList());
        }

       
    }

    public static class Pagination
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index, int pageSize)
        {
            return new PagedList<T>(source, index, pageSize);
        }

        public static PagedList<T> ToPagedList<T>(this IQueryable<T> source, int index)
        {
            return new PagedList<T>(source, index, 10);
        }
    }
}