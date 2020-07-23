using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Entities;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.MVC.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        // GET
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task Favorite(UserFavoriteRequestModel requestModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                await _userService.FavoriteMovie(requestModel.MovieId, userId);
                LocalRedirect($"~/Movie/Detail/{requestModel.MovieId}");
            }
            else
            {
                LocalRedirect("~/Account/Login");
            }
        }

        [HttpPost]
        public async Task Unfavorite(UserFavoriteRequestModel requestModel)
        {
            if (User.Identity.IsAuthenticated)
            {
                var userId = Convert.ToInt32(HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);
                await _userService.UnfavoriteMovie(requestModel.MovieId, userId);
                LocalRedirect($"~/Movie/Detail/{requestModel.MovieId}");
            }
            else
            {
                LocalRedirect("~/Account/Login");
            }
        }
    }
}