using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
