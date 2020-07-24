using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.Infrastructure.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;

        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<IEnumerable<Movie>> GetAllPurchasedMovie(int userId)
        {
            var movies = await _purchaseRepository.GetPurchasedMovieByUser(userId);
            return movies;
        }
    }
}