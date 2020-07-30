using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;
        private readonly IPurchaseService _purchaseService;

        public UserController(IUserService userService, IReviewService reviewService, IPurchaseService purchaseService)
        {
            _userService = userService;
            _reviewService = reviewService;
            _purchaseService = purchaseService;
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> PurchaseMovie([FromBody] UserPurchaseRequestModel model)
        {
            await _userService.PurchaseMovie(model);
            return Ok();
        }
        
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetUserPurchasedMovies([FromHeader] int userId)
        {
            // ToDo [refactor getting user identity]
            var purchases = await _purchaseService.GetAllPurchasedMovie(userId);
            return Ok(purchases);
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> FavoriteMovie([FromBody] UserFavoriteRequestModel model)
        {
            await _userService.FavoriteMovie(model.MovieId, model.UserId);
            return Ok();
        }
        
        [HttpPost]
        [Route("unfavorite")]
        public async Task<IActionResult> UnFavoriteMovie([FromBody] UserFavoriteRequestModel model)
        {
            await _userService.RemoveFavoriteMovie(model.MovieId, model.UserId);
            return Ok();
        }

        [HttpGet]
        [Route("favorites/{id}")]
        public async Task<IActionResult> GetUserFavoriteMovies(int id)
        {
            // ToDo [refactor getting user identity]
            var movies = await _userService.GetUserFavoriteMovies(id);
            return Ok(movies);
        }

        [HttpPost]
        [Route("leave/review")]
        public async Task<IActionResult> WriteReview([FromBody] ReviewRequestModel model)
        {
            var review = await _reviewService.WriteReview(model);
            return Ok(review);
        }

        [HttpPut]
        [Route("update/review")]
        public async Task<IActionResult> UpdateReview(ReviewRequestModel model)
        {
            var review = await _reviewService.UpdateReview(model);
            return Ok(review);
        }

        [HttpDelete]
        [Route("delete/review")]
        public async Task<IActionResult> DeleteReview(ReviewRequestModel model)
        {
            await _reviewService.DeleteReview(model);
            return Ok("The review has been deleted");
        }
        
        [HttpGet]
        [Route("reviews/{id}")]
        public async Task<IActionResult> GetUserReviewedMovies(int id)
        {
            // ToDo [refactor getting user identity]
            var reviews = await _userService.GetUserReviewedMovies(id);
            // ToDo [return reviews will throw error]
            // A possible object cycle was detected which is not supported.
            var response = reviews.Select(r => new
                {r.MovieId, r.Movie.Title, r.Movie.PosterUrl, r.Rating, r.ReviewText});
            return Ok(response);
        }
        
        [HttpGet]
        [Route("{userId}/movie/{movieId}/favorite")]
        public async Task<IActionResult> IsFavorite(int userId, int movieId)
        {
            var isFavorite = await _userService.IsMovieFavoriteByUser(userId, movieId);
            return Ok(new {IsFavorite = isFavorite});
        }
    }
}