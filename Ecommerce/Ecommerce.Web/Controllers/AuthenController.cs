using Ecommerce.Web.Commons;
using Ecommerce.Web.Models.Authen;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class AuthenController : Controller
    {
        private readonly IAuthenService _service;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IConfiguration _configuration;

        public AuthenController(IAuthenService service, IHttpContextAccessor contextAccessor, IConfiguration configuration)
        {
            _service = service;
            _contextAccessor = contextAccessor;
            _configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            var sessionLogin = _contextAccessor.HttpContext.Session.GetString(Constants.SessionKey.sessionLogin);
            if (sessionLogin != null)
                return RedirectToAction("Home", "Index");

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login req)
        {
            var result = await _service.Login(req);
            result.returnUrl = Url.Content("~" + result.returnUrl);

            return Json(result);
        }

        public IActionResult Logout()
        {
            var cookieName = _configuration.GetValue<string>("AppSettings:CookieName");
            _contextAccessor.HttpContext.Session.Clear();
            Response.Cookies.Delete(cookieName);

            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register req)
        {
            var result = await _service.Register(req);
            return Json(result);
        }
    }
}
