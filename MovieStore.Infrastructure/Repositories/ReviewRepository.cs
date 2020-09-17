using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;

namespace MovieStore.Infrastructure.Repositories
{
    public class ReviewRepository : EfRepository<Review>, IReviewRepository
    {
        public ReviewRepository(MoviesStoreDbContext dbContext) : base(dbContext)
        {
        }

        public override async Task<IEnumerable<Review>> ListAsync(Expression<Func<Review, bool>> filter)
        {
            // Review entity will probably be updated later.
            return await _dbContext.Reviews.Where(filter).Include(r => r.Movie).ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetUserReviewedMovies(int userId)
        {
            var reviewedMovies =
                await _dbContext.Reviews.Where(r => r.UserId == userId).Include(r => r.Movie).Include(r => r.User).ToListAsync();
            return reviewedMovies;
        }

        public async Task<Review> IsReviewedByUserAsync(int userId, int movieId)
        {
            return await _dbContext.Reviews.FirstOrDefaultAsync(r => r.UserId == userId && r.MovieId == movieId);
        }
    }
}