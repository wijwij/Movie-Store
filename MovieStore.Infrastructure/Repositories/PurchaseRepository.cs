using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Infrastructure.Data;

namespace MovieStore.Infrastructure.Repositories
{
    public class PurchaseRepository : EfRepository<Purchase>, IPurchaseRepository
    {
        public PurchaseRepository(MoviesStoreDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<Movie>> GetPurchasedMovieByUser(int userId)
        {
            // Purchase entity can be updated, like user apply for refund.
            var movies = await _dbContext.Purchases.Where(p => p.UserId == userId).Include(p => p.Movie).Select(p => p.Movie).ToListAsync();
            return movies;
        }
    }
}