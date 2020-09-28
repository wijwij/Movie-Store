using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;

namespace MovieStore.Infrastructure.Repositories
{
    public class MovieCastRepository : EfRepository<MovieCast>, IMovieCastRepository
    {
        public MovieCastRepository(MoviesStoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<MovieCast>> GetCastsByMovieIdAsync(int movieId)
        {
            return await _dbContext.MovieCasts.Where(mc => mc.MovieId == movieId).Include(mc => mc.Cast).ToListAsync();
        }
    }
}