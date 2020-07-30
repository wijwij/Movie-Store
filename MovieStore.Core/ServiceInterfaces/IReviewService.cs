using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IReviewService
    {
        Task<Review> WriteReview(ReviewRequestModel requestModel);
        Task<Review> UpdateReview(ReviewRequestModel requestModel);
        Task DeleteReview(ReviewRequestModel requestModel);
        Task<IEnumerable<Review>> GetReviews(int userId);
    }
}