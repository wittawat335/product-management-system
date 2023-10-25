using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    [Route("ErrorPages/{statuscode}")]
    public class ErrorPagesController : Controller
    {
        public IActionResult Index(int statuscode)
        {
            string action = string.Empty;
            switch (statuscode)
            {
                case 401:
                    action = "UnAuthorized";
                    break;
                case 403:
                    action = "Forbidden";
                    break;
                case 404:
                    action = "NotFound";
                    break;
                case 500:
                    action = "ServerError";
                    break;
                default:
                    break;
            }

            return View(action);
        }
    }
}
