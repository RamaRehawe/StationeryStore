using Microsoft.AspNetCore.Mvc;
using StationeryStore.Models;
using System.Threading.Tasks;

namespace StationeryStore.Views.Singup
{
    public class SignupController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View(); // Assuming you have a corresponding View file named Signup.cshtml
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string email, string password)
        {
             var user = new User { Username = username, Email = email, Password = password };
             await _userRepository.AddUserAsync(user);
             await _emailService.SendConfirmationEmailAsync(email, confirmationCode);
            return RedirectToAction("Confirmation"); // Redirect to a confirmation page
        }

        public IActionResult Confirmation()
        {
            ViewBag.Message = "تم التسجيل بنجاح!";
            return View(); // Assuming you have a corresponding View file named Confirmation.cshtml
        }
    }
}
