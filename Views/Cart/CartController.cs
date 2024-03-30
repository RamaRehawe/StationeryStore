using Microsoft.AspNetCore.Mvc;

namespace StationeryStore.Views.Cart
{
    public class CartController : Controller
    {
        [HttpGet]
        [Route("front_cart")]
        public IActionResult Index()
        {
            return View("Cart");
        }
    }
}
