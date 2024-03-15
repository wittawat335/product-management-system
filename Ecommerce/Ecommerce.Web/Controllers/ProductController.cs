using Ecommerce.Web.Commons;
using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Models;
using Ecommerce.Web.Models.Category;
using Ecommerce.Web.Models.Product;
using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Ecommerce.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IBaseApiService<Product> _productService;
        private readonly IBaseApiService<Category> _categoryService;
        private readonly IWebHostEnvironment _environment;
        private readonly AppSetting _setting;

        public ProductController(
            IBaseApiService<Product> productService,
            IBaseApiService<Category> categoryService,
            IOptions<AppSetting> options,
            IWebHostEnvironment environment)
        {
            _productService = productService;
            _categoryService = categoryService;
            _environment = environment;
            _setting = options.Value;
        }

        public IActionResult Index() 
        {
            var session = HttpContext.Session.GetString(Constants.SessionKey.sessionLogin);
            if (string.IsNullOrEmpty(session)) return RedirectToAction("Login", "Authen");
            return View(); 
        }

        [HttpPost]
        public async Task<IActionResult> _PopUpDialog(string id, string action)
        {
            var response = new Response<Product>();
            var model = new ProductViewModel();

            var listCategory = await 
                _categoryService.GetListAsync(_setting.BaseApiUrl + "Category");
            if (listCategory != null) 
                ViewBag.listCategory = listCategory.value.Where(x => x.Status == "A");

            if (!string.IsNullOrEmpty(id))
                response = await 
                    _productService
                    .GetAsyncById(_setting.BaseApiUrl + string.Format("Product/{0}", id));

            if (response.value != null) model.Product = response.value;

            model.Action = action;
            return PartialView(model);
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
            string uploadFolder = 
                Path.Combine(_environment.WebRootPath, "images\\product\\" + productId);
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

        [HttpPost]
        public void DeleteImageFolder(string id)
        {
            string path = Path.Combine(_environment.WebRootPath, "images\\product\\" + id);
            if (Directory.Exists(path)) Directory.Delete(path, true);
        }
    }
}
