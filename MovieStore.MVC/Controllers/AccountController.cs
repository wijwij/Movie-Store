using System.Threading.Tasks;
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
                    ModelState.AddModelError(string.Empty, "Invalid login");
                }
            }
            return View();
        }
    }
}