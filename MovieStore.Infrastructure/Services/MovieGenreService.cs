using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.Infrastructure.Services
{
    public class MovieGenreService : IMovieGenreService
    {
        protected readonly IMovieGenreRepository _movieGenreRepository;
        
        public MovieGenreService(IMovieGenreRepository movieGenreRepository)
        {
            _movieGenreRepository = movieGenreRepository;
        }
        public async Task<IEnumerable<Movie>> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieGenreRepository.GetMoviesByGenreIdAsync(genreId);
            return movies;
        }

        public async Task<IEnumerable<Genre>> GetGenresByMovie(int movieId)
        {
            var genres = await _movieGenreRepository.GetGenresByMovieIdAsync(movieId);
            return genres;
        }
    }
}