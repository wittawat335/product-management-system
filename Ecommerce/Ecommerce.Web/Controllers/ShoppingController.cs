using Ecommerce.Web.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Web.Controllers
{
    public class ShoppingController : Controller
    {
        private readonly IProductService _productService;

        public ShoppingController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            //int pageSize = 3;
            //ViewBag.PageNumber = p;
            //ViewBag.PageRange = pageSize;
            //ViewBag.CategorySlug = categorySlug;
            //var productList = await _productService.GetListProduct(null);
            //var categoryList = await _productService.GetListCategory();

            //if (categorySlug == "")
            //{
            //    ViewBag.TotalPages = (int)Math.Ceiling((decimal)productList.value.Count() / pageSize);
            //    return View(await _productService.GetListShopping(pageSize, p));
            //}

            //var category = categoryList.value.FirstOrDefault(c => c.CategoryName == categorySlug);
            //if (category == null) return RedirectToAction("Index");

            //var productsByCategory = _context.Products.Where(p => p.CategoryId == category.Id);
            //ViewBag.TotalPages = (int)Math.Ceiling((decimal)productsByCategory.Count() / pageSize);

            return View();
        }
    }
}
