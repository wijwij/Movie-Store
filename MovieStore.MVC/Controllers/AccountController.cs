using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MovieStore.Core.Models.Request;
using MovieStore.Core.ServiceInterfaces;

namespace MovieStore.MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService _userService;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Register(UserRegisterRequestModel userRegisterRequestModel)
        {
            // server side evaluation
            if (ModelState.IsValid)
            {
                // we take this object from the View
                var createdUser = await _userService.RegisterUser(userRegisterRequestModel);
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestModel loginRequest)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.ValidateUser(loginRequest.Email, loginRequest.Password);
                
                if (user == null)
                {
                    // Bug don't return error message and continue next code block, raise ArugumentNullException
                    ModelState.AddModelError(string.Empty, "Invalid login");
                    // temp solution
                    return View();
                }
                
                // A claim is a statement about an entity
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.GivenName, user.FirstName),
                    new Claim(ClaimTypes.Surname, user.LastName),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Email),
                };
                
                // create an Identity object to hold those claims. A ClaimsIdentity --> identifier of a person
                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                
                // create cookie that will be attached to the Http Response
                // ToDo search HttpContext, holds all the info regarding that Http request/response
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));
                // can manually creating cookie, SigInAsync helps to encrypt the info
                // HttpContext.Response.Cookies.Append("userlanguage", value);
                
                // Once ASP.NET Creates Authentication Cookies, it will check for that cookie in the HttpRequest and see if the cookie is not expired
                // and it will decrypt the information present in the cookie to check whether User is Authenticated and will also get claims from the cookies
                
                return LocalRedirect("~/");
            }
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return LocalRedirect("~/");
        }
    }
}