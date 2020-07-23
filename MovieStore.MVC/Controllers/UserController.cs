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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Favorite(UserFavoriteRequestModel requestModel)
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            await _userService.FavoriteMovie(requestModel.MovieId, userId);

            return Redirect($"~/Movies/Details/{requestModel.MovieId}");
        }

        [HttpPost]
        public async Task<IActionResult> Unfavorite(UserFavoriteRequestModel requestModel)
        {
            var userId = Convert.ToInt32(HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
            await _userService.UnfavoriteMovie(requestModel.MovieId, userId);

            return Redirect($"~/Movies/Details/{requestModel.MovieId}");
        }

        [HttpGet("/User/{id}/Movie/{movieId}/favorite")]
        public async Task IsFavorite(int id, int movieId)
        {
            try
            {
                var isFavorite = await _userService.IsFavorite(id, movieId);
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

        /*
         * Filters (attribute) in ASP.NET. Some piece of code that runs either before an controller or action method executes or when some event happens.
         * 1. authorization
         * 2. action filter
         * 3. result filter
         * 4. exception filter(only catch in the controller), but in real world, we use exception middleware.
         * 5. resource filter
         */
    }
}