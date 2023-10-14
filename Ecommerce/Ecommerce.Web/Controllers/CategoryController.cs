using Ecommerce.Web.Commons;
using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecommerce.Web.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBaseApiService<Category> _categoryService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly AppSetting _setting;

        public CategoryController(IBaseApiService<Category> categoryService, IHttpContextAccessor contextAccessor,
            IOptions<AppSetting> options)
        {
            _categoryService = categoryService;
            _contextAccessor = contextAccessor;
            _setting = options.Value;
        }

        public IActionResult Index()
        {
            var sessionLogin = _contextAccessor.HttpContext.Session.GetString(Constants.SessionKey.sessionLogin);
            if (sessionLogin == null)
                return RedirectToAction("Login", "Authen");

            return View();
        }

        public async Task<IActionResult> GetList()
        {
            var response = await _categoryService.GetListAsync(_setting.BaseApiUrl + "Category/GetList");
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> _PopUpDialog(string id, string action)
        {
            var model = new CategoryViewModel();
            var response = new Response<Category>();
            try
            {
                if (!string.IsNullOrEmpty(id))
                    response = await _categoryService.GetAsyncById(_setting.BaseApiUrl + string.Format("Category/GetCategory/{0}", id));

                model.Category = response.value;
                model.Action = action;
            }
            catch
            {
                throw;
            }

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(CategoryViewModel model)
        {
            var response = new Response<Category>();
            try
            {
                if (model != null)
                {
                    switch (model.Action)
                    {
                        case Constants.Action.Add:
                            response = await _categoryService.InsertAsync(_setting.BaseApiUrl + "Category/Add", model.Category);
                            break;
                        case Constants.Action.Update:
                            response = await _categoryService.PutAsync(_setting.BaseApiUrl + "Category/Update", model.Category);
                            break;
                        default:
                            response.message = Constants.MessageError.CallAPI;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                response.message = ex.Message;
            }

            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _categoryService.DeleteAsync(_setting.BaseApiUrl + string.Format("Category/Delete/{0}", id));
            return Json(response);
        }
    }
}
