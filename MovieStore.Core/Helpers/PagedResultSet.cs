using System.Collections.Generic;

namespace MovieStore.Core.Helpers
{
    public class PagedResultSet<TModel> where TModel : class
    {
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public long TotalCount { get; set; }
        
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public IEnumerable<TModel> Data { get; set; }
        
    }
}