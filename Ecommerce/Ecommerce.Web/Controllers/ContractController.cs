using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ContractController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
