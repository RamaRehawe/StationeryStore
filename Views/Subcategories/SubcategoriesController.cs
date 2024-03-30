using Microsoft.AspNetCore.Mvc;

namespace StationeryStore.Views.Subcategories
{
    public class SubcategoriesController : Controller
    {
        [HttpGet]
        [Route("front_pens")]
        public IActionResult Index()
        {
            return View("Pens");
        }

        [HttpGet]
        [Route("front_office")]
        public IActionResult Index1()
        {
            return View("Office");
        }

        [HttpGet]
        [Route("front_ruler")]
        public IActionResult Index2()
        {
            return View("Ruler");
        }
        [HttpGet]
        [Route("front_notes")]
        public IActionResult Index3()
        {
            return View("Notes");
        }

        [HttpGet]
        [Route("front_notebooks")]
        public IActionResult Index4()
        {
            return View("Notebooks");
        }

        [HttpGet]
        [Route("front_schoolbags")]
        public IActionResult Index5()
        {
            return View("Schoolbags");
        }

        [HttpGet]
        [Route("front_brushs")]
        public IActionResult Index6()
        {
            return View("Brushs");
        }
    }
}
