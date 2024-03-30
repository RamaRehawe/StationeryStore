using Microsoft.AspNetCore.Mvc;

namespace StationeryStore.Views.DriverDashboard
{
    public class DriverDashboardController : Controller
    {
        [HttpGet]
        [Route("front_driver")]
        public IActionResult Index()
        {
            return View("DriverDashboard");
        }
    }
}
