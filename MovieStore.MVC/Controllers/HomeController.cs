using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc; // where Controller class gets defined
using Microsoft.Extensions.Logging;
using MovieStore.MVC.Models;

namespace MovieStore.MVC.Controllers
{
    // any class can be a MVC controller if it inherits from Controller base class
    // routing--- Pattern matching technique
    // http://localhost:2323/Home/index, HomeController, Index--action method
    public class HomeController : Controller
    {
        [Route("[action]")]
        [Route("hello")]
        public IActionResult Index()
        {
            // return an instance of a class that implements that interface
            // By default it will into Views folder --> Home --> Index.cshtml
            /*
             * 1. Program.cs --> main method
             * 2. build() method in main --> ConfigureServices() in Startup class
             * 3. runt() method in main --> Configure() in Startup class
             * 5. find HomeController
             * 6. call Action method
             * 7. Return a view
             *
             * In ASP.NET core middleware... a piece of software logic that will be executed...
             */
            
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}