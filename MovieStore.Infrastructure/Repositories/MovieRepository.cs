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
            var movie = await _dbContext.Movies.Where(m => m.Id == id).Include(m => m.MovieGenres).ThenInclude(mg => mg.Genre)
                .Include(m => m.MovieCasts).ThenInclude(mc => mc.Cast)
                .Include(m => m.Reviews).AsNoTracking().FirstOrDefaultAsync();
            // populate the rating field.
            if(movie != null) movie.Rating = Decimal.Round(movie.Reviews.Average(r => r.Rating), 2);
            return movie;
        }

        public async Task<IEnumerable<Movie>> GetHighestRevenueMovies()
        {
            var movies = await _dbContext.Movies.OrderByDescending(m => m.Revenue).Take(25).Select(m => new Movie {Id = m.Id, PosterUrl = m.PosterUrl, Title = m.Title}).AsNoTracking().ToListAsync();
            return movies;
        }

        public async Task<IEnumerable<Movie>> GetHighestRatedMovies()
        {
            // ToDo [make a comparison between two approaches]
            var collections = await _dbContext.Movies.Include(m => m.Reviews).Select(m => new Movie{Id = m.Id, PosterUrl = m.PosterUrl, Title = m.Title, Rating = m.Reviews.Average(r => r.Rating)})
                .OrderByDescending(m => m.Rating)
                .Take(25).AsNoTracking().ToListAsync();

            // var movies = await _dbContext.Reviews.Include(r => r.Movie)
            //     .GroupBy(r => new {r.Movie.Id, r.Movie.PosterUrl, r.Movie.Title})
            //     .OrderByDescending(g => g.Average(r => r.Rating))
            //     .Select(m => new Movie
            //     {
            //         Id = m.Key.Id,
            //         PosterUrl = m.Key.PosterUrl,
            //         Title = m.Key.Title,
            //         Rating = m.Average(r => r.Rating)
            //     }).Take(25).ToListAsync();
            return collections;
        }

        public async Task<int> GetMoviesCount(string title)
        {
            return await _dbContext.Movies.Where(m => m.Title.Equals(title)).CountAsync();
        }
    }
}