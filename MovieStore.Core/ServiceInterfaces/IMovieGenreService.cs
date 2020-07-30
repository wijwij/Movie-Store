using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Response;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IMovieGenreService
    {
        Task<IEnumerable<MovieCardResponseModel>> GetMoviesByGenre(int genreId);
        Task<IEnumerable<GenreResponseModel>> GetGenresByMovie(int movieId);
    }
}