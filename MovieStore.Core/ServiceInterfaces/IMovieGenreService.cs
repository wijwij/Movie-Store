using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IMovieGenreService
    {
        Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId);
        Task<IEnumerable<Genre>> GetGenresByMovie(int movieId);
    }
}