using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Models.Request;
using MovieStore.Core.Models.Response;
using MovieStore.Core.ServiceInterfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace MovieStore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public AccountController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] UserRegisterRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.ValidationState);
            // when posting json data, make sure your json keys are identical with c# model properties.
            // case insensitive
            // in MVC, name of the input in HTML should be same as c# model properties 
            var user = await _userService.RegisterUser(model);
            if (user == null) return Unauthorized(new {errorMessage = "Email address already exists. Please try to login"});
            return Ok(user);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> ValidateUser([FromBody] LoginRequestModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState.ValidationState);
            var user = await _userService.ValidateUser(model.Email, model.Password);
            if (user == null) return Unauthorized(new {errorMessage = "Invalid Credentials"});
            // return the JWT token
            return Ok(new {token = GenerateJWT(user)});
        }

        private string GenerateJWT(LoginResponseModel payload)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.GivenName, payload.FirstName),
                new Claim(ClaimTypes.Surname, payload.LastName),
                new Claim(ClaimTypes.NameIdentifier, payload.Id.ToString()),
                new Claim(ClaimTypes.Email, payload.Email),
            };
            var identityClaim = new ClaimsIdentity(claims);
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenSettings:PrivateKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var expires = DateTime.UtcNow.AddHours(_configuration.GetValue<double>("TokenSettings:ExpirationHours"));
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identityClaim,
                Expires = expires,
                SigningCredentials = credentials,
                Issuer = _configuration["TokenSettings:Issuer"],
                Audience = _configuration["TokenSettings:Audience"]
            };
            var encodedJwt = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(encodedJwt);
        }
    }
}