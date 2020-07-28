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

        [HttpPost]
        [Route("review")]
        public async Task<IActionResult> WriteReview([FromBody] ReviewRequestModel model)
        {
            await _reviewService.WriteReview(model);
            return Ok();
        }
        
        [HttpGet]
        [Route("reviews/{id}")]
        public async Task<IActionResult> Reviews(int id)
        {
            var reviews = await _reviewService.GetReviews(id);
            return Ok(reviews);
        }
        
        [HttpGet]
        [Route("{userId}/movie/{movieId}/favorite")]
        public async Task<IActionResult> IsFavorite(int userId, int movieId)
        {
            var isFavorite = await _userService.IsFavorite(userId, movieId);
            return Ok(new {IsFavorite = isFavorite});
        }
    }
}