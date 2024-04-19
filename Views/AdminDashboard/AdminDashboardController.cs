using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace StationeryStore.Views.AdminDashboard
{
    public class AdminDashboardController : Controller
    {
        [HttpGet]
        [Route("front_Admin")]
        //[Authorize (Roles = "Admin")]
        public IActionResult Index()
        {
            return View("AdminDashboard");
        }
    }
}
