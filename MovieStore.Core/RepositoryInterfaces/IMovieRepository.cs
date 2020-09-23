using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Response;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetHighestRevenueMovies();
        Task<IEnumerable<Movie>> GetHighestRatedMovies();
        Task<int> GetMoviesCount(string title);
        Task<IEnumerable<RatedMovieCardResponseModel>> GetMoviesAboveRatingAsync(decimal rating);
    }
}