using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<Movie>> GetAllPurchasedMovie(int userId);
    }
}