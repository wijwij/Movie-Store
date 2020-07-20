using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IMovieGenreRepository
    {
        Task<IEnumerable<Movie>> GetMoviesByGenreIdAsync(int genreId);
        Task<IEnumerable<Genre>> GetGenresByMovieIdAsync(int movieId);
    }
}