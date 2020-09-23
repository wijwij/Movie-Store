using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Response;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IMovieService
    {
        /*
         * expose functionality that are needed by client
         */
        Task<IEnumerable<MovieCardResponseModel>> GetHighestGrossingMovies();
        Task<IEnumerable<MovieCardResponseModel>> GetTop25RatedMovies();
        Task<MovieDetailResponseModel> GetMovieById(int movieId, int? userId = null);
        Task<Movie> CreateMovie(Movie movie);
        Task<Movie> UpdateMovie(Movie movie);
        Task<int> GetMoviesCount(string title = "");
        Task<IEnumerable<RatedMovieCardResponseModel>> GetMoviesAboveRating(decimal rating);
    }
}