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
        public async Task<Review> WriteReview(ReviewRequestModel requestModel)
        {
            var review = new Review
            {
                UserId = requestModel.UserId, MovieId = requestModel.MovieId, Rating = requestModel.Rating,
                ReviewText = requestModel.ReviewText
            };
            return await _reviewRepository.AddAsync(review);
        }

        public async Task<IEnumerable<Review>> GetReviews(int userId)
        {
            var reviews = await _reviewRepository.ListAsync(r => r.UserId == userId);
            return reviews;
        }

        public async Task<Review> UpdateReview(ReviewRequestModel requestModel)
        {
            var review = new Review
            {
                UserId = requestModel.UserId, MovieId = requestModel.MovieId, Rating = requestModel.Rating,
                ReviewText = requestModel.ReviewText
            };
            return await _reviewRepository.UpdateAsync(review);
        }

        public async Task DeleteReview(ReviewRequestModel requestModel)
        {
            var review = new Review
            {
                UserId = requestModel.UserId, MovieId = requestModel.MovieId, Rating = requestModel.Rating,
                ReviewText = requestModel.ReviewText
            };
            await _reviewRepository.DeleteAsync(review);
        }
    }
}