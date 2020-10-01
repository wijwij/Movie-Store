using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieStore.Core.Helpers
{
    public class PaginatedList<T>
    {
        public PaginatedList(int pageIndex, int pageSize, long totalCount, IQueryable<T> query)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);
            TotalCount = totalCount;
            Query = query;
        }

        public int PageIndex { get; }
        public int PageSize { get;}
        public int TotalPages { get; }
        public long TotalCount { get; }
        public IQueryable<T> Query { get;}

        public static async Task<PaginatedList<T>> GetPagedList(IQueryable<T> source, int pageIndex, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderQuery, Expression<Func<T, bool>> filter)
        {
            #region construct-query
            if (filter != null) source = source.Where(filter);
            if (orderQuery != null) source = orderQuery(source);
            var query = source.Skip((pageIndex - 1) * pageSize).Take(pageSize);
            #endregion
            
            long count = await source.CountAsync();

            return new PaginatedList<T>(pageIndex, pageSize, count, query);
        }
    }
}