using Microsoft.AspNetCore.Mvc;

namespace StationeryStore.Views
{
    public class ItemController : Controller
    {
        [HttpGet]
        [Route("front_item")]
        public IActionResult Index()
        {
            return View("Item");
        }
    }
}
