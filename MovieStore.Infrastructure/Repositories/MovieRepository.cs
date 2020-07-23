using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;

namespace MovieStore.Infrastructure.Repositories
{
    public class MovieRepository : EfRepository<Movie>, IMovieRepository
    {
        
        public MovieRepository(MoviesStoreDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<Movie> GetByIdAsync(int id)
        {
            var movie = await _dbContext.Movies.Where(m => m.Id == id).Include(m => m.MovieGenres)
                .Include(m => m.MovieCasts).ThenInclude(mc => mc.Cast)
                .Include(m => m.Reviews).Include(m => m.Purchases)
                .Include(m => m.Favorites).FirstOrDefaultAsync();
            // populate the rating field.
            movie.Rating = movie.Reviews.Average(r => r.Rating);
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(25).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetHighestRatedMovies()
        {
            var collections = await _dbContext.Movies.Include(m => m.Reviews).OrderByDescending(m => m.Reviews.Average(r => r.Rating))
                .Take(25).ToListAsync();
            // var alternative = await _dbContext.Reviews.Include(r => r.Movie).GroupBy(r => r.MovieId)
            //     .Select(g => new {MovieId = g.Key, Rating = g.Average(r => r.Rating)}).Take(25)
            //     .Join(_dbContext.Movies, t => t.MovieId, m => m.Id, (t, m) => m).ToListAsync();
            return collections;
        }

        public async Task<int> GetMoviesCount(string title)
        {
            return await _dbContext.Movies.Where(m => m.Title.Equals(title)).CountAsync();
        }
    }
}