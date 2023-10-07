﻿using Ecommerce.Web.Commons;
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

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(Login req)
        {
            var result = await _service.Login(req);
            result.returnUrl = Url.Content(result.returnUrl);

            return Json(result);
        }

        public IActionResult Logout()
        {
            Response.Cookies.Delete(Constants.SessionKey.sessionLogin);
            Response.Cookies.Delete(Constants.SessionKey.accessToken);
            Response.Clear();

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
