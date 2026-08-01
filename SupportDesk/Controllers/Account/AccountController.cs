using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportDesk.Data.ViewModels;
using SupportDesk.Services;

namespace SupportDesk.Controllers.Account
{
    [Route("account")]
    public class AccountController(AccountService accountService) : Controller
    {
        [HttpGet("")]
        [Authorize]
        public IActionResult Index()
        {
            var vm = new AccountViewModel("Placeholder full name", "Placeholder email", "Placeholder role");

            return View("account", vm);
        }

        [HttpGet("login")]
        public async Task<IActionResult> LoginAsync()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index");
            }

            var result = await accountService.Login(
                model.Email,
                model.Password,
                model.RememberMe);

            if (!result.Succeeded)
            {
                return BadRequest("Invalid email or password.");
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("register")]
        public async Task<IActionResult> RegisterAsync()
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (User.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Home");
            }

            string fullName = string.IsNullOrWhiteSpace(model.Infix)
                ? $"{model.FirstName} {model.LastName}"
                : $"{model.FirstName} {model.Infix} {model.LastName}";

            var result = await accountService.Register(model.Email, fullName, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return RedirectToAction("Index", "Home");
        }

        [HttpGet("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await accountService.Logout();

            return RedirectToAction("Login");
        }
    }
}
