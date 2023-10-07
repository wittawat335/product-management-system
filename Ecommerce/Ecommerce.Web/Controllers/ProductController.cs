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
        private readonly IProductService _service;
        private readonly IWebHostEnvironment _environment;
        private readonly AppSetting _setting;

        public ProductController(
            IBaseApiService<Product> productService,
            IBaseApiService<Category> categoryService,
            IOptions<AppSetting> options,
            IWebHostEnvironment environment,
            IProductService service)
        {
            _productService = productService;
            _categoryService = categoryService;
            _setting = options.Value;
            _environment = environment;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Test()
        {
            return View();
        }

        public async Task<IActionResult> GetList()
        {
            var response = await _productService.GetListAsync(_setting.BaseApiUrl + "Product/GetList");
            return Json(response);
        }

        [HttpPost]
        public async Task<IActionResult> _PopUpDialog(string id, string action)
        {
            var response = new Response<Product>();
            var model = new ProductViewModel();
            try
            {
                if (!string.IsNullOrEmpty(id))
                    response = await _productService.GetAsyncById(_setting.BaseApiUrl + string.Format("Product/GetProduct/GetProduct?id={0}", id));

                var listCategory = await _categoryService.GetListAsync(_setting.BaseApiUrl + "Category/GetListActive");
                model.Product = response.value;
                model.listCategory = listCategory.value;
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
            if (model.Product.ImageFile != null)
                model.Product.Image = UploadFile(model.Product);

            var response = await _service.Save(model);

            return RedirectToAction(nameof(Index));
        }

        private string UploadFile(Product model)
        {
            string uploadFolder = Path.Combine(_environment.WebRootPath, "images\\product");
            if (!Directory.Exists(uploadFolder))
                Directory.CreateDirectory(uploadFolder);

            string filePath = Path.Combine(uploadFolder, model.ImageFile.FileName);
            if (System.IO.File.Exists(filePath))
                System.IO.File.Delete(filePath);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                model.ImageFile.CopyTo(fileStream);
            }

            return model.ImageFile.FileName;
        }
    }
}
