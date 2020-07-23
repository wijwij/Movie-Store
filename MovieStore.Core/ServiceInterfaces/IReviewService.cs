using System.Threading.Tasks;
using MovieStore.Core.Models.Request;

namespace MovieStore.Core.ServiceInterfaces
{
    public interface IReviewService
    {
        Task WriteReview(ReviewRequestModel requestModel, int userId);
    }
}