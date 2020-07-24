using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IReviewService
    {
        Task WriteReview(ReviewRequestModel requestModel, int userId);
        Task<IEnumerable<Review>> GetReviews(int userId);
    }
}