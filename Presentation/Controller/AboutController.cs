namespace StationeryStore.Presentation.Controller
{
    public class AboutController : Controller
    {
        public IActionResult Index()
        {
            ViewData["Vision"] = "نسعى لتوفير أفضل مستلزمات القرطاسية بجودة عالية وأسعار معقولة، لتلبية احتياجات عملائنا وتسهيل عمليات التعلم والإبداع.";
            ViewData["Mission"] = "نحن نعمل على توفير تجربة تسوق ممتازة لعملائنا، من خلال تقديم منتجات ذات جودة عالية وخدمة عملاء استثنائية.";
            ViewData["Values"] = new[] { "الجودة: نحن نلتزم بتقديم منتجات عالية الجودة.", "الخدمة: نحن نسعى لتقديم خدمة عملاء ممتازة.", "الشفافية: نحن نؤمن بالشفافية والنزاهة في جميع عملياتنا." };
            ViewData["ContactEmail"] = "info@stationerystore.com";

            return View("About");
        }
    }
}
