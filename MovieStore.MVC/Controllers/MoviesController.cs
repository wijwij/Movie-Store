using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Entities;
using MovieStore.Core.ServiceInterfaces;
using MovieStore.MVC.Models;

namespace MovieStore.MVC.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly IMovieGenreService _movieGenreService;

        // IOC, ASP.NET Core has built-in IOC/DI (.NET framework needs a third party IOC, eg. Autofac, Ninject)
        public MoviesController(IMovieService movieService, IMovieGenreService movieGenreService)
        {
            _movieService = movieService;
            _movieGenreService = movieGenreService;
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var movie = await _movieService.GetMovieById(id);
            return View(movie);
        }

        [HttpGet]
        public async Task<IActionResult> Genres(int id)
        {
            var movies = await _movieGenreService.GetMoviesByGenre(id);
            return View(movies);
        }

        /*
         * ToDo [review]
         * three ways to send data from model to view
         *   1. strongly-typed models
         *   2. ViewBag - dynamic
         *   3. view data - key/value
         *
         * Usually it prefer to send a strongly typed Model or Object.
         * why works with strong-typed type? Because developers want to perform type check during compile time instead of raising error during runtime.
         */
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var movies = await _movieService.GetTop25RatedMovies();
            // var movies = await _movieService.GetTop25RatedMovies();
            return View(movies);
        }
        
        /*
         * ToDo [review]
         * Model Binding (microsoft build for us): case in-sensitive
         * map the in-coming requests input elements key/value with the parameters of the action method.
         * it will also perform casting and converting
         */
        [HttpPost]
        public IActionResult Create(string title, decimal budget)
        {
            return View("Create");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}