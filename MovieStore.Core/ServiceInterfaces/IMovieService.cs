using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IMovieService
    {
        /*
         * expose functionality that are needed by client
         */
        Task<IEnumerable<Movie>> GetHighestGrossingMovies();
        Task<IEnumerable<Movie>> GetTop25RatedMovies();
        Task<Movie> GetMovieById(int id);
        Task<Movie> CreateMovie(Movie movie);
        Task<Movie> UpdateMovie(Movie movie);
        Task<int> GetMoviesCount(string title = "");
    }
}