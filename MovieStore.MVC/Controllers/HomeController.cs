using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; // where Controller class gets defined
using Microsoft.Extensions.Logging;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.MVC.Models;

namespace MovieStore.MVC.Controllers
{
    // any class can be a MVC controller if it inherits from Controller base class
    // routing--- Pattern matching technique
    // http://localhost:2323/Home/index, HomeController, Index--action method
    public class HomeController : Controller
    {
        private readonly IMovieService _movieService;

        public HomeController(IMovieService movieService)
        {
            _movieService = movieService;
        }
        public async Task<IActionResult> Index()
        {
            /*
             * [ToDo] [refactor notes]
             * 1. Program.cs --> main method
             * 2. build() method in main --> ConfigureServices() in Startup class
             * 3. runt() method in main --> Configure() in Startup class
             * 5. find HomeController
             * 6. call Action method
             * 7. Return a view
             *
             * In ASP.NET core middleware... a piece of software logic that will be executed...
             */
            var movies = await _movieService.GetHighestGrossingMovies();
            /*
             * Movie Card
             *  - Home page -- show top revenue movies -- movie card
             *  - Genres/show movies belong to that genre -- movie card
             *  - Top rated Movie -- movie card
             *
             * Partial view (view inside parent view) allows to reuse view. 
             */
            return View(movies);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}