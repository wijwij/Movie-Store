using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace MovieStore.Core.Helpers
{
    public class PaginatedList<T> : List<T>
    {
        public PaginatedList(IList<T> items, int pageIndex, int pageSize, long totalCount)
        {
            PageIndex = pageIndex;
            TotalPages = (int) Math.Ceiling(totalCount / (double) pageSize);
            TotalCount = totalCount;
            AddRange(items);
        }

        public int PageIndex { get; }
        public int TotalPages { get; }
        public long TotalCount { get; }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public async static Task<PaginatedList<T>> GetPagedList(IQueryable<T> source, int pageIndex, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderQuery, Expression<Func<T, bool>> filter)
        {
            if (filter != null) source = source.Where(filter);
            if (orderQuery != null) source = orderQuery(source);
            
            long count = await source.CountAsync();
            var items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            return new PaginatedList<T>(items, pageIndex, pageSize, count);
        }
    }
}