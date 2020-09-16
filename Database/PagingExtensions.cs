using System.Collections.Generic;
using System.Linq;

namespace Database
{
    /// <summary>
    /// Extension class for paging
    /// </summary>
    public static class PagingExtensions
    {
        /// <summary>
        /// Method to get results by pagesize and pagenumber
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="pageNumber"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public static IQueryable<TSource> DoPaging<TSource>(this IQueryable<TSource> source, int pageNumber, int pageSize)
        {
            return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        //used by LINQ
        public static IEnumerable<TSource> DoPaging<TSource>(this IEnumerable<TSource> source, int pageNumber, int pageSize)
        {
            return source.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

    }
}
