using Ecommerce.Web.Commons;
using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Ecommerce.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IBaseApiService<Menu> _MenuService;
        private readonly AppSetting _setting;

        public HomeController(ILogger<HomeController> logger,
            IWebHostEnvironment environment,
            IHttpContextAccessor contextAccessor,
            IBaseApiService<Menu> MenuService,
            IOptions<AppSetting> options)
        {
            _logger = logger;
            _environment = environment;
            _contextAccessor = contextAccessor;
            _MenuService = MenuService;
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
            List<TreeViewNode> nodes = new List<TreeViewNode>();
            var response = await _MenuService.GetListAsync(_setting.BaseApiUrl + "Menu/GetListActive");

            foreach (var item in response.value.Where(x => x.MenuLevel == 1).OrderBy(x => x.MenuOrder)) //Lv1
            {
                nodes.Add(new TreeViewNode
                {
                    id = item.MenuId.ToString(),
                    parent = item.ParentId.ToString(),
                    text = item.MenuName
                });
            }
            foreach (var item in response.value.Where(x => x.MenuLevel == 2).OrderBy(x => x.MenuOrder)) //Lv2
            {
                nodes.Add(new TreeViewNode
                {
                    id = item.MenuId.ToString(),
                    parent = item.ParentId.ToString(),
                    text = item.MenuName
                });
            }
            ViewBag.Json = JsonConvert.SerializeObject(nodes);

            return View();
        }

        [HttpPost]
        public IActionResult TestJsTree(string selectedItem)
        {
            List<TreeViewNode> item = JsonConvert.DeserializeObject<List<TreeViewNode>>(selectedItem);
            return Json(item);
        }
    }
}