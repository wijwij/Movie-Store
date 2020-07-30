using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Response;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<MovieCardResponseModel>> GetAllPurchasedMovie(int userId);
    }
}