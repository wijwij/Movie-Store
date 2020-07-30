using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GenresController : ControllerBase
    {
        private readonly IGenreService _genreService;
        private readonly IMovieGenreService _movieGenreService;

        public GenresController(IGenreService genreService, IMovieGenreService movieGenreService)
        {
            _genreService = genreService;
            _movieGenreService = movieGenreService;
        }
        
        [Route("")]
        [HttpGet]
        public async Task<IActionResult> GetAllGenres()
        {
            var genres = await _genreService.GetAllGenres();
            return Ok(genres);
        }

        [HttpGet]
        [Route("movies/{genreId}")]
        public async Task<IActionResult> GetMoviesByGenre(int genreId)
        {
            var movies = await _movieGenreService.GetMoviesByGenre(genreId);
            return Ok(movies);
        }
    }
}