using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
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

        public async Task<Review> UpdateReview(ReviewRequestModel requestModel)
        {
            var review = new Review
            {
                UserId = requestModel.UserId, MovieId = requestModel.MovieId, Rating = requestModel.Rating,
                ReviewText = requestModel.ReviewText
            };
            return await _reviewRepository.UpdateAsync(review);
        }

        public async Task<bool> DeleteReview(int userId, int movieId)
        {
            var review = await _reviewRepository.ListAsync(r => r.MovieId == movieId && r.UserId == userId);
            var result = await _reviewRepository.DeleteAsync(review.FirstOrDefault());
            return result > 0;
        }
    }
}