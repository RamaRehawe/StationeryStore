using Microsoft.AspNetCore.Mvc;

namespace StationeryStore.Views.CustomerService
{
    public class CustomerServiceController : Controller
    {
        [HttpGet]
        [Route("front_customer_service")]
        public IActionResult Index()
        {
            return View("CustomerService") ;
        }
    }
}
