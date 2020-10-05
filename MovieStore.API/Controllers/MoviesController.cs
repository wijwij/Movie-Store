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
        private readonly ICurrentUserService _currentUserService;

        public MoviesController(IMovieService movieService, IMovieGenreService movieGenreService, ICurrentUserService currentUserService)
        {
            _movieService = movieService;
            _movieGenreService = movieGenreService;
            _currentUserService = currentUserService;
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> GetMovies([FromQuery] int pageIndex = 1, [FromQuery] int pageSize = 30, [FromQuery] string title = "", [FromQuery] string orderBy = "")
        {
            var movies = await _movieService.GetMoviesByPagination(pageIndex, pageSize, title, orderBy);
            return Ok(movies);
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
            var movie = await _movieService.GetMovieById(id, _currentUserService.Id);
            return Ok(movie);
        }

        [HttpGet]
        [Route("genres/{movieId}")]
        public async Task<IActionResult> GetGenresByMovie(int movieId)
        {
            var genres = await _movieGenreService.GetGenresByMovie(movieId);
            return Ok(genres);
        }

        [HttpGet]
        [Route("rating")]
        public async Task<IActionResult> GetMoviesAboveRating([FromQuery] decimal rating)
        {
            var movies = await _movieService.GetMoviesAboveRating(rating);
            return Ok(movies);
        }
    }
}