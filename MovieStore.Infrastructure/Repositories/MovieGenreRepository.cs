using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;

namespace MovieStore.Infrastructure.Repositories
{
    public class MovieGenreRepository : IMovieGenreRepository
    {
        protected readonly MoviesStoreDbContext _dbContext;

        public MovieGenreRepository(MoviesStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public async Task<IEnumerable<Movie>> GetMoviesByGenreIdAsync(int genreId)
        {
            // Eager loading will improve the query performance.
            // ToDo [Add pagination]
            var movies = await _dbContext.MovieGenres.Where(mg => mg.GenreId == genreId).Include(mg => mg.Movie)
                .Select(mg => mg.Movie).AsNoTracking().ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Genre>> GetGenresByMovieIdAsync(int movieId)
        {
            var genres = await _dbContext.MovieGenres.Where(mg => mg.MovieId == movieId).Include(mg => mg.Genre)
                .Select(mg => mg.Genre).AsNoTracking().ToListAsync();
            return genres;
        }
    }
}