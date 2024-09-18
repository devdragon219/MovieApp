using Microsoft.AspNetCore.Mvc;
using MovieAppBackend.Services;
using MovieAppBackend.DTOs;
using System.Threading.Tasks;

namespace MovieAppBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MoviesController : ControllerBase
    {
        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public IActionResult GetMovies()
        {
            var movies = _movieService.GetAllMovies();
            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([FromForm] MovieDTO movieDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var movie = await _movieService.CreateMovie(movieDTO);
            return CreatedAtAction(nameof(GetMovies), new { id = movie.Id }, movie);
        }
    }
}
