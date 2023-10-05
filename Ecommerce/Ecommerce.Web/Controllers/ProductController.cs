using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Services;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IBaseApiService<Product> _productService;
        private readonly AppSetting _setting;

        public ProductController(IBaseApiService<Product> productService, IOptions<AppSetting> options)
        {
            _productService = productService;
            _setting = options.Value;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetImage(string productId)
        {
            return Json(await _productService.GetAsyncById(_setting.BaseApiUrl + string.Format("Product/GetImage/{0}", productId)));
        }

        [HttpPost]
        public IActionResult _PopUpDialog(string id, string action)
        {
            var model = new Product();
            return PartialView(model);
        }

        public async Task<IActionResult> GetList(Product filter = null)
        {
            var response = await _productService.GetListAsync(_setting.BaseApiUrl + "Product/GetList", filter);
            return Json(response);
        }
    }
}
