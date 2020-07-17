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

        public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(25).ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetHighestRatedMovies()
        {
            // ToDo [Test - too complex LINQ query]
            var topMovies = _dbContext.Reviews.GroupBy(r => r.MovieId)
                .Select(g => new {MovieId = g.Key, Rating = g.Average(r => r.Rating)}).OrderByDescending(r => r.Rating)
                .Take(25);
            var collections = await topMovies.Join(_dbContext.Movies, tm => tm.MovieId, m => m.Id, (tm, m) => new {Rating = tm.Rating, Movie = m})
                .OrderByDescending(t => t.Rating).Select(t => t.Movie).ToListAsync();
            return collections;
        }

        public async Task<int> GetMoviesCount(string title)
        {
            return await _dbContext.Movies.Where(m => m.Title.Equals(title)).CountAsync();
        }
    }
}