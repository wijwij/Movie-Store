using System;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.MVC.Controllers
{
    [Authorize]
    public class UserController : Controller
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

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Favorite(UserFavoriteRequestModel requestModel)
        {
            // var userId = Convert.ToInt32(HttpContext.User.Claims
            //     .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var userId = _currentUserService.Id ?? 0;
            await _userService.FavoriteMovie(requestModel.MovieId, userId);

            return Redirect($"~/Movies/Details/{requestModel.MovieId}");
        }

        [HttpPost]
        public async Task<IActionResult> Unfavorite(UserFavoriteRequestModel requestModel)
        {
            // var userId = Convert.ToInt32(HttpContext.User.Claims
            //     .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var userId = _currentUserService.Id ?? 0;
            await _userService.RemoveFavoriteMovie(requestModel.MovieId, userId);

            return Redirect($"~/Movies/Details/{requestModel.MovieId}");
        }

        [HttpGet("/User/{id}/Movie/{movieId}/favorite")]
        public async Task IsFavorite(int id, int movieId)
        {
            try
            {
                var isFavorite = await _userService.IsMovieFavoriteByUser(id, movieId);
                // write to the response
                HttpContext.Response.StatusCode = (int) HttpStatusCode.OK;
                await HttpContext.Response.WriteAsync($"{isFavorite}");
            }
            catch (Exception e)
            {
                HttpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                throw;
            }
        }

        [HttpPost]
        public async Task Review(ReviewRequestModel requestModel)
        {
            // var userId = Convert.ToInt32(HttpContext.User.Claims
            //     .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var userId = _currentUserService.Id ?? 0;
            requestModel.UserId = userId;
            await _reviewService.WriteReview(requestModel);
        }

        /*
         * ToDo [review filters]
         * Filters (attribute) in ASP.NET. Some piece of code that runs either before an controller or action method executes or when some event happens.
         * 1. authorization
         * 2. action filter
         * 3. result filter
         * 4. exception filter(only catch in the controller), but in real world, we use exception middleware.
         * 5. resource filter
         */

        [HttpGet]
        public async Task<IActionResult> Reviews()
        {
            // var userId = Convert.ToInt32(HttpContext.User.Claims
            //     .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var userId = _currentUserService.Id ?? 0;
            var reviews = await _userService.GetUserReviewedMovies(userId);
            return View(reviews);
        }

        [HttpPost]
        public async Task<IActionResult> Purchase(UserPurchaseRequestModel requestModel)
        {
            // var userId = Convert.ToInt32(HttpContext.User.Claims
            //     .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var userId = _currentUserService.Id ?? 0;
            requestModel.UserId = userId;
            await _userService.PurchaseMovie(requestModel);
            return View();
        }

        public async Task<IActionResult> Purchases()
        {
            // var userId = Convert.ToInt32(HttpContext.User.Claims
            //     .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            var userId = _currentUserService.Id ?? 0;
            var movies = await _purchaseService.GetAllPurchasedMovie(userId);
            return View(movies);
        }
    }
}