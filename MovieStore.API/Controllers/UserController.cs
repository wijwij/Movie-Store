using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IReviewService _reviewService;
        private readonly IPurchaseService _purchaseService;
        private readonly ICurrentUserService _currentUserService;

        public UserController(IUserService userService, IReviewService reviewService, IPurchaseService purchaseService, ICurrentUserService currentUserService)
        {
            _userService = userService;
            _reviewService = reviewService;
            _purchaseService = purchaseService;
            _currentUserService = currentUserService;
        }

        [HttpPost]
        [Route("purchase")]
        public async Task<IActionResult> PurchaseMovie([FromBody] UserPurchaseRequestModel model)
        {
            model.UserId = _currentUserService.Id ?? 0;
            var purchase = await _userService.PurchaseMovie(model);
            if (purchase == null) return BadRequest(new {errorMessage = "You have already purchased the movie."});
            return Ok();
        }
        
        [HttpGet]
        [Route("purchases")]
        public async Task<IActionResult> GetUserPurchasedMovies()
        {
            var userId = _currentUserService.Id ?? 0;
            var purchases = await _purchaseService.GetAllPurchasedMovie(userId);
            return Ok(purchases);
        }

        [HttpPost]
        [Route("favorite")]
        public async Task<IActionResult> FavoriteMovie([FromBody] UserFavoriteRequestModel model)
        {
            model.UserId = _currentUserService.Id ?? 0;
            var favorite = await _userService.FavoriteMovie(model.MovieId, model.UserId);
            if (favorite == null) return BadRequest(new {errorMessage = "You have already liked the movie."});
            return Ok();
        }
        
        [HttpDelete]
        [Route("unfavorite/{movieId}")]
        public async Task<IActionResult> UnFavoriteMovie([FromRoute] int movieId)
        {
            var result = await _userService.RemoveFavoriteMovie(movieId, _currentUserService.Id ?? 0);
            if (result) return Ok();
            return BadRequest(new {errorMessage = "You haven't ever liked the movie."});
        }

        [HttpGet]
        [Route("favorites")]
        public async Task<IActionResult> GetUserFavoriteMovies()
        {
            var id = _currentUserService.Id ?? 0;
            var movies = await _userService.GetUserFavoriteMovies(id);
            return Ok(movies);
        }

        [HttpPost]
        [Route("leave/review")]
        public async Task<IActionResult> WriteReview([FromBody] ReviewRequestModel model)
        {
            model.UserId = _currentUserService.Id ?? 0;
            var review = await _reviewService.WriteReview(model);
            return Ok(review);
        }

        [HttpPut]
        [Route("update/review")]
        public async Task<IActionResult> UpdateReview(ReviewRequestModel model)
        {
            model.UserId = _currentUserService.Id ?? 0;
            var review = await _reviewService.UpdateReview(model);
            return Ok(review);
        }

        [HttpDelete]
        [Route("delete/review/{movieId}")]
        public async Task<IActionResult> DeleteReview([FromRoute] int movieId)
        {
            var result = await _reviewService.DeleteReview(_currentUserService.Id ?? 0, movieId);
            if (!result) return BadRequest(new {errorMessage = "You haven't ever leave a review."});
            return Ok();
        }
        
        [HttpGet]
        [Route("reviews")]
        public async Task<IActionResult> GetUserReviewedMovies()
        {
            var id = _currentUserService.Id ?? 0;
            var reviews = await _userService.GetUserReviewedMovies(id);
            return Ok(reviews);
        }
        
        [HttpGet]
        [Route("{userId}/movie/{movieId}/favorite")]
        public async Task<IActionResult> IsFavorite(int userId, int movieId)
        {
            var isFavorite = await _userService.IsMovieFavoriteByUser(userId, movieId);
            return Ok(new {IsFavorite = isFavorite});
        }

        [HttpGet]
        [Route("profile")]
        public async Task<IActionResult> GetUserProfile()
        {
            var id = _currentUserService.Id ?? 0;
            var profile = await _userService.GetUserProfile(id);
            return Ok(profile);
        }
    }
}