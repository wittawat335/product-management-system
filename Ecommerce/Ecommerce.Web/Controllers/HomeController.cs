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
        private readonly AppSetting _setting;

        public HomeController(ILogger<HomeController> logger,
            IWebHostEnvironment environment,
            IOptions<AppSetting> options)
        {
            _logger = logger;
            _environment = environment;
            _setting = options.Value;
        }

        public IActionResult CheckEnvironment()
        {
            return Content("Environment : " + _environment.EnvironmentName);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }




    }
}