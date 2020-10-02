using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Helpers;
using MovieStore.Core.Models.Response;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetHighestRevenueMovies();
        Task<IEnumerable<Movie>> GetHighestRatedMovies();
        Task<int> GetMoviesCount(string title);
        Task<IEnumerable<RatedMovieCardResponseModel>> GetMoviesAboveRatingAsync(decimal rating);

        Task<PagedResultSet<MovieCardResponseModel>> GetPaginatedMoviesBySearchTitle(int pageIndex, int pageSize,
            Func<IQueryable<Movie>, IOrderedQueryable<Movie>> orderQuery = null,
            Expression<Func<Movie, bool>> filter = null);
    }
}