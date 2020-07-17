using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IMovieRepository : IAsyncRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetHighestRevenueMovies();
        Task<IEnumerable<Movie>> GetHighestRatedMovies();
        Task<int> GetMoviesCount(string title);
    }
}