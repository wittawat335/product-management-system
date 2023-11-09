using Ecommerce.Web.Commons;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class DashboardController : Controller
    {

        public DashboardController()
        {
        }

        public IActionResult Index()
        {
            var sessionLogin = HttpContext.Session.GetString(Constants.SessionKey.sessionLogin);
            if (sessionLogin == null)
                return RedirectToAction("Login", "Authen");

            return View();
        }
    }
}
