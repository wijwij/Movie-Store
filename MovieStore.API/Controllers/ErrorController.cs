using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MovieStore.API.Controllers
{
    [ApiController]
    [Route("/error")]
    public class ErrorController: ControllerBase
    {
        [Route("")]
        [AllowAnonymous]
        public IActionResult HandleException()
        {
            return StatusCode(500);
        }
    }
}