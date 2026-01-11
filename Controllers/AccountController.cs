using Microsoft.AspNetCore.Mvc;

namespace GymManager.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet("/login")]
        public IActionResult Login()
        {
            return Redirect("/Identity/Account/Login");
        }

        [HttpGet("/register")]
        public IActionResult Register()
        {
            return Redirect("/Identity/Account/Register");
        }

        [HttpGet("/logout")]
        public IActionResult Logout()
        {
            return Redirect("/Identity/Account/Logout");
        }
    }
}
