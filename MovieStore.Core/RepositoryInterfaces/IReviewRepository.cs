using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IReviewRepository : IAsyncRepository<Review>
    {
        Task<IEnumerable<Review>> GetUserReviewedMovies(int userId);
        Task<Review> IsReviewedByUserAsync(int userId, int movieId);
    }
}