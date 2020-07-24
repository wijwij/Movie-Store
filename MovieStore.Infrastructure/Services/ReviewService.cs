using System.Collections.Generic;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.RepositoryInterfaces;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.Infrastructure.Services
{
    public class ReviewService : IReviewService
    {
        private readonly IReviewRepository _reviewRepository;

        public ReviewService(IReviewRepository reviewRepository)
        {
            _reviewRepository = reviewRepository;
        }
        public async Task WriteReview(ReviewRequestModel requestModel, int userId)
        {
            var review = new Review
            {
                UserId = userId, MovieId = requestModel.MovieId, Rating = requestModel.Rating,
                ReviewText = requestModel.ReviewText
            };
            await _reviewRepository.AddAsync(review);
        }

        public async Task<IEnumerable<Review>> GetReviews(int userId)
        {
            var reviews = await _reviewRepository.ListAsync(r => r.UserId == userId);
            return reviews;
        }
    }
}