using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using MovieStore.MVC.Models;

namespace MovieStore.MVC.Controllers
{
    public class MoviesController : Controller
    {
        // GET localhost/Movies/Index
        [HttpGet]
        // public IActionResult Index()
        // {
        //     var movies = new List<Movie>
        //     {
        //         new Movie {Id = 1, Title = "Avengers: Infinity War", Budget = 1200000},
        //         new Movie {Id = 2, Title = "Avatar", Budget = 1200000},
        //         new Movie {Id = 3, Title = "Star Wars: The Force Awakens", Budget = 1200000},
        //         new Movie {Id = 4, Title = "Titanic", Budget = 1200000},
        //         new Movie {Id = 5, Title = "Inception", Budget = 1200000},
        //         new Movie {Id = 6, Title = "Avengers: Age of Ultron", Budget = 1200000},
        //         new Movie {Id = 7, Title = "Interstellar", Budget = 1200000},
        //         new Movie {Id = 8, Title = "Fight Club", Budget = 1200000},
        //     };
        //     /*
        //      * three ways to send data from model to view
        //      *   1. strongly-typed models
        //      *   2. ViewBag - dynamic
        //      *   3. view data - key/value
        //      *
        //      * Usually it prefer to send a strongly typed Model or Object.
        //      * why works with strong-typed type? Because developers want to perform type check during compile time instead of raising error during runtime.
        //      */
        //     
        //     // ViewBag to send some metadata.
        //     ViewBag.MoviesCount = movies.Count;
        //     // dictionary
        //     ViewData["myname"] = "John Doe";
        //
        //     return View(movies);
        // }

        [HttpPost]
        public IActionResult Create(string title, decimal budget)
        {
            // we need to get the data from view
            Console.WriteLine($"Receiving title: {title}");
            Console.WriteLine($"Budget is: {budget}");
            /*
             * ToDo [review]
             * Model Binding (microsoft build for us): case in-sensitive
             * map the in-coming requests input elements key/value with the parameters of the action method.
             * it will also perform casting and converting
             */
            
            // save to database
            return View("Create");
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
    }
}