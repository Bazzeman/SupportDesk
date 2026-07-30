using Microsoft.AspNetCore.Mvc;

namespace SupportDesk.Controllers.Privacy
{
    [Route("privacy")]
    public class PrivacyController : Controller
    {
        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
