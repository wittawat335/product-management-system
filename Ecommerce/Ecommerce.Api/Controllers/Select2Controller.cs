using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Select2Controller : ControllerBase
    {
        private readonly IProductService _productservice;
        private readonly ICategoryService _categoryService;

        public Select2Controller(IProductService productservice, ICategoryService categoryService)
        {
            _productservice = productservice;
            _categoryService = categoryService;
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(string request)
        {
            var response = await _productservice.GetList();
            if (response.value != null) 
                response.value = response.value.Where(x => x.ProductName.ToLower().Contains(request.ToLower())).ToList();

            return Ok(response);
        }


        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory(string request)
        {
            var response = await _categoryService.GetList();
            if (response.value != null)
                response.value = response.value.Where(x => x.CategoryName.ToLower().Contains(request.ToLower())).ToList();

            return Ok(response);
        }
    }
}
