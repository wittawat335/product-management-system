using Ecommerce.Web.Commons;
using Ecommerce.Web.Models;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class AuthenController : Controller
    {
        private readonly IAuthenService _service;

        public AuthenController(IAuthenService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login model)
        {
            var result = await _service.Login(model);
            return new JsonResult(result);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete(Constants.SessionKey.sessionLogin);
            Response.Cookies.Delete(Constants.SessionKey.accessToken);
            Response.Clear();

            return RedirectToAction("Login");
        }

        [HttpPost]
        public IActionResult Register()
        {
            return View();
        }
    }
}
