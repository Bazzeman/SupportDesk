using Microsoft.AspNetCore.Mvc;
using SupportDesk.Services;

namespace SupportDesk.Controllers.User
{
    [Route("user")]
    public class UserController(ApplicationUserService userService) : Controller
    {
        [HttpGet("")]
        public IActionResult Index() =>
            RedirectToAction("login");

        [HttpGet("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            var result = await userService.Login(
                email,
                password,
                false);

            if (!result.Succeeded)
            {
                return BadRequest("Invalid email or password.");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await userService.Logout();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("register")]
        public async Task<IActionResult> RegisterAsync(string email, string fullName, string password)
        {
            var result = await userService.Register(email, fullName, password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
