using Microsoft.AspNetCore.Mvc;

namespace StationeryStore.Views.Home
{
    public class HomeController : Controller
    {
        [HttpGet]
        [Route("front_home")]
        public IActionResult Index()
        {
            return View("Home");
        }
    }
}
