using Microsoft.AspNetCore.Mvc;

namespace StationeryStore.Views.Login
{
    public class LoginController : Controller
    {
        [HttpGet]
        [Route("front_login")]
        public IActionResult Index()
        {
            return View("Login") ;
        }
    }
}
