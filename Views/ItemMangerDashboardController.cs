using Microsoft.AspNetCore.Mvc;

namespace StationeryStore.Views.ItemMangerDashboard
{
    public class ItemMangerDashboardController : Controller
    {
        [HttpGet]
        [Route("front_item_manger")]
        public IActionResult Index()
        {
            return View("ItemMangerDashboard");
        }
    }
}
