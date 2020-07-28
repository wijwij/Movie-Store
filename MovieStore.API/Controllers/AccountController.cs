using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            // when posting json data, make sure your json keys are identical with c# model properties.
            // case insensitive
            // in MVC, name of the input in HTML should be same as c# model properties 
            var user = await _userService.RegisterUser(model);
            return Ok(user);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> ValidateUser([FromBody] LoginRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var user = await _userService.ValidateUser(model.Email, model.Password);
            return Ok(user);
        }
    }
}