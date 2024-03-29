using Microsoft.AspNetCore.Mvc;

namespace StationeryStore.Views.PrivacyPolicy
{

    public class PrivacyPolicyController : Controller
    {
        [HttpGet]
        [Route("front_privacy_policy")]
        public IActionResult Index()
        {
            ViewData["Title"] = "سياسة الخصوصية";
            ViewData["Introduction"] = "نحن في متجر القرطاسية نلتزم بحماية خصوصية زوار وعملاء موقعنا. يرجى قراءة سياسة الخصوصية بعناية لفهم كيفية جمع واستخدام المعلومات الشخصية.";
            ViewData["DataCollection"] = "نجمع المعلومات التي تقدمها لنا عند إنشاء حساب أو عندما تقوم بالشراء، مثل الاسم وعنوان البريد الإلكتروني وعنوان الشحن.";
            ViewData["DataUsage"] = "نستخدم المعلومات لتقديم الخدمات وتسهيل عمليات الشراء وتحسين تجربة التسوق عبر الموقع.";
            ViewData["DataSharing"] = "نحن لا نشارك المعلومات مع أطراف ثالثة دون موافقتك الصريحة، باستثناء الحالات التي تكون ذلك ضروريًا لتقديم الخدمات (مثل خدمات الشحن).";
            ViewData["DataSecurity"] = "نحن نتخذ إجراءات أمان لحماية معلوماتك الشخصية ونحن نلتزم بالحفاظ على سرية هذه المعلومات.";
            ViewData["UserRights"] = "لديك الحق في الوصول إلى معلوماتك الشخصية وتحديثها أو حذفها. يمكنك أيضًا تعيين تفضيلات الاتصال الخاصة بك.";
            ViewData["Updates"] = "قد نقوم بتحديث سياسة الخصوصية من حين لآخر، وسنقوم بإعلامك بأي تغييرات مهمة عبر البريد الإلكتروني أو من خلال إشعار على الموقع.";

            return View("PrivacyPolicy");
        }
    }
}