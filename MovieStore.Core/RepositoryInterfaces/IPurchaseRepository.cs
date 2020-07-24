using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.RepositoryInterfaces
{
    public interface IPurchaseRepository : IAsyncRepository<Purchase>
    {
        Task<IEnumerable<Movie>> GetPurchasedMovieByUser(int userId);
    }
}