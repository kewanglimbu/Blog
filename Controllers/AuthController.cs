using BlogWebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BlogWebApp.Controllers
{
    public class AuthController : Controller
    {
        private readonly SignInManager<IdentityUser> _signInManager;

        public AuthController(SignInManager<IdentityUser> signInManager)
        {
            
            _signInManager = signInManager;
            
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View(new LoginViewModel());
        }


     [HttpPost]
     public async Task<IActionResult> LoginAsync(LoginViewModel loginvm)
        {
            var result= await _signInManager.PasswordSignInAsync(loginvm.UserName, loginvm.Password,false,false);
            return RedirectToAction("Index","Home");
        }


        [HttpGet]
        public async Task<IActionResult> Logout() 
        {
            await  _signInManager.SignOutAsync();
            return RedirectToAction("Index","Home");
        }

    }
}
