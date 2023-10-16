using Ecommerce.Web.Commons;
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
        private readonly IBaseApiService<Category> _categoryService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IProductService _service;
        private readonly IWebHostEnvironment _environment;
        private readonly AppSetting _setting;

        public ProductController(
            IBaseApiService<Product> productService,
            IBaseApiService<Category> categoryService,
            IOptions<AppSetting> options,
            IWebHostEnvironment environment,
            IHttpContextAccessor contextAccessor,
            IProductService service)
        {
            _productService = productService;
            _categoryService = categoryService;
            _contextAccessor = contextAccessor;
            _setting = options.Value;
            _environment = environment;
            _service = service;
        }

        public IActionResult Index()
        {
            var sessionLogin = _contextAccessor.HttpContext.Session.GetString(Constants.SessionKey.sessionLogin);
            if (sessionLogin == null)
                return RedirectToAction("Login", "Authen");

            return View();
        }

        public async Task<IActionResult> GetList(Product search)
        {
            var response = await _service.GetListProduct(search);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> _PopUpDialog(string id, string action)
        {
            var response = new Response<Product>();
            var model = new ProductViewModel();
            try
            {
                var listCategory = await _categoryService.GetListAsync(_setting.BaseApiUrl + "Category/GetListActive");
                if (listCategory != null)
                    model.listCategory = listCategory.value;

                if (!string.IsNullOrEmpty(id))
                    response = await _productService.GetAsyncById(_setting.BaseApiUrl + string.Format("Product/GetProduct/{0}", id));

                model.Product = response.value;
                model.Action = action;
            }
            catch
            {
                throw;
            }

            return PartialView(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductViewModel model)
        {
            var response = await _service.Save(model);
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _productService.DeleteAsync(_setting.BaseApiUrl + string.Format("Product/Delete/{0}", id));
            if (response.isSuccess)
                DeleteImageFolder(id);

            return Json(response);
        }

        [HttpPost]
        public IActionResult SaveImage(IFormFile file, string productId, string message)
        {
            var response = new Response<string>();
            if (file != null && productId != null)
                UploadFile(file, productId);

            response.isSuccess = true;
            response.message = message;
            return Json(response);
        }

        private void UploadFile(IFormFile image, string productId)
        {
            string uploadFolder = Path.Combine(_environment.WebRootPath, "images\\product\\" + productId);
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            string filePath = Path.Combine(uploadFolder, image.FileName);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
        }

        private void DeleteImageFolder(string productId)
        {
            string path = Path.Combine(_environment.WebRootPath, "images\\product\\" + productId);
            if (Directory.Exists(path)) Directory.Delete(path, true);
        }

        public async Task<IActionResult> select2Product(string query)
        {
            var response = await _service.Select2Product(query);
            return Json(response);
        }

        public async Task<IActionResult> select2Category(string query)
        {
            var response = await _service.Select2Category(query);
            return Json(response);
        }
    }
}
