using Microsoft.AspNetCore.Mvc;
using StationeryStore.Models;
using System.Threading.Tasks;

namespace StationeryStore.Views.Singup
{
    public class SignupController : Controller
    {
        [HttpGet]
        [Route("front_signup")]
        public IActionResult Index()
        {
            return View("Signup"); // Assuming you have a corresponding View file named Signup.cshtml
        }

    }
}
