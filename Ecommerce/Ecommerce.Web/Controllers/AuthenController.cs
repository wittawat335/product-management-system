using Ecommerce.Web.Commons;
using Ecommerce.Web.Models.Authen;
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

        public IActionResult Login() { return View(); }

        [HttpPost]
        public async Task<IActionResult> Login(Login req)
        {
            var result = await _service.Login(req);
            if (result.isSuccess) await _service.GetPermission(result.value.positionId);
            //result.returnUrl = Url.Content("~" + result.returnUrl);

            return Json(result);
        }

        public IActionResult Logout()
        {
            clearSession();
            return RedirectToAction("Login");
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register req)
        {
            var result = await _service.Register(req);
            return Json(result);
        }

        public void clearSession()
        {
            HttpContext.Session.Remove(Constants.SessionKey.sessionLogin);
            HttpContext.Session.Remove(Constants.SessionKey.accessToken);
            HttpContext.Session.Remove(Constants.SessionKey.permission);
            HttpContext.Session.Clear();
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
        }
    }
}
