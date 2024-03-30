using Microsoft.AspNetCore.Mvc;

namespace StationeryStore.Views.ForgotPassword
{
    public class ForgotPasswordController : Controller
    {
        [HttpGet]
        [Route("front_forgot_password")]
        public IActionResult Index()
        {
            return View("ForgotPassword");
        }
    }
}
