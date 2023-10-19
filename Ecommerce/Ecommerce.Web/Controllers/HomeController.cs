using Ecommerce.Web.Commons;
using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Reflection;

namespace Ecommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPermissionService _permissionService;
        private readonly AppSetting _setting;

        public HomeController(ILogger<HomeController> logger,
            IWebHostEnvironment environment,
            IHttpContextAccessor contextAccessor,
            IPermissionService permissionService,
            IOptions<AppSetting> options)
        {
            _logger = logger;
            _environment = environment;
            _contextAccessor = contextAccessor;
            _permissionService = permissionService;
            _setting = options.Value;
        }

        public IActionResult CheckEnvironment()
        {
            return Content("Environment : " + _environment.EnvironmentName);
        }

        public IActionResult Index()
        {
            var sessionLogin = _contextAccessor.HttpContext.Session.GetString(Constants.SessionKey.sessionLogin);
            if (sessionLogin == null)
                return RedirectToAction("Login", "Authen");

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public async Task<IActionResult> TestJsTree()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> JsTree(string positionId)
        {
            positionId = "P01";
            var response = await _permissionService.GetJsTree(_setting.BaseApiUrl + string.Format("Position/GetJsTree/{0}", positionId));
            return Json(response.value);
        }

    }
}