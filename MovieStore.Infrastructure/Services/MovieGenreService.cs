using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Response;
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
        public async Task<IEnumerable<MovieCardResponseModel>> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieGenreRepository.GetMoviesByGenreIdAsync(genreId);
            var response = movies.Select(m => new MovieCardResponseModel
                {Id = m.Id, Title = m.Title, PosterUrl = m.PosterUrl});
            return response;
        }

        public async Task<IEnumerable<GenreResponseModel>> GetGenresByMovie(int movieId)
        {
            var genres = await _movieGenreRepository.GetGenresByMovieIdAsync(movieId);
            var response = genres.Select(g => new GenreResponseModel {Id = g.Id, Name = g.Name});
            return response;
        }
    }
}