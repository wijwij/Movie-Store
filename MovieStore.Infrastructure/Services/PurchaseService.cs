using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Response;
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
        public async Task<IEnumerable<MovieCardResponseModel>> GetAllPurchasedMovie(int userId)
        {
            var movies = await _purchaseRepository.GetPurchasedMovieByUser(userId);
            var response = movies.Select(m => new MovieCardResponseModel
                {Id = m.Id, Title = m.Title, PosterUrl = m.PosterUrl});
            return response;
        }
    }
}