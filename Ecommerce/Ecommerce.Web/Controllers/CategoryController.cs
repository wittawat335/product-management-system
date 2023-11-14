using Ecommerce.Web.Commons;
using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Models.Category;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecommerce.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBaseApiService<Category> _categoryService;
        private readonly AppSetting _setting;

        public CategoryController(IBaseApiService<Category> categoryService, IOptions<AppSetting> options)
        {
            _categoryService = categoryService;
            _setting = options.Value;
        }

        public IActionResult Index() { return View(); }

        [HttpPost]
        public async Task<IActionResult> _PopUpDialog(string id, string action)
        {
            var model = new CategoryViewModel();
            var response = new Response<Category>();
            if (!string.IsNullOrEmpty(id))
                response = await _categoryService.GetAsyncById(_setting.BaseApiUrl + string.Format("Category/GetCategory/{0}", id));

            if (response.value != null) model.Category = response.value;

            model.Action = action;
            return PartialView(model);
        }
    }
}
