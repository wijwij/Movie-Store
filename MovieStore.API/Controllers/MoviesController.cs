using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.API.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly IMovieGenreService _movieGenreService;

        public MoviesController(IMovieService movieService, IMovieGenreService movieGenreService)
        {
            _movieService = movieService;
            _movieGenreService = movieGenreService;
        }

        [HttpGet]
        [Route("toprevenue")]
        public async Task<IActionResult> GetTopRevenueMovies()
        {
            var movies = await _movieService.GetHighestGrossingMovies();
            if (!movies.Any()) return NotFound("No movies are found!");
            // the built-in Ok() return json type with 200 HTTP status code
            return Ok(movies);
        }
        
        [HttpGet]
        [Route("toprated")]
        public async Task<IActionResult> GetTopRatedMovies()
        {
            var movies = await _movieService.GetTop25RatedMovies();
            if (!movies.Any()) return NotFound("No movies are found!");
            return Ok(movies);
        }

        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> GetMovieDetailById([FromRoute] int id)
        {
            var movie = await _movieService.GetMovieById(id);
            return Ok(movie);
        }

        [HttpGet]
        [Route("genre/{genreId}")]
        public async Task<IActionResult> GetMoviesByGenre([FromRoute] int genreId)
        {
            var movies = await _movieGenreService.GetMoviesByGenre(genreId);
            return Ok(movies);
        }
    }
}