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
        private readonly IUserService _userService;
        private readonly IPositionService _positionService;

        public Select2Controller(IProductService productservice, ICategoryService categoryService, IUserService userService, IPositionService positionService)
        {
            _productservice = productservice;
            _categoryService = categoryService;
            _userService = userService;
            _positionService = positionService;
        }

        [HttpGet("GetProduct")]
        public async Task<IActionResult> GetProduct(string request)
        {
            var response = await _productservice.GetList();
            if (response.isSuccess) 
                response.value = response.value.Where(x => x.ProductName.ToLower().Contains(request.ToLower())).ToList();

            return Ok(response);
        }

        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategory(string request)
        {
            var response = await _categoryService.GetList();
            if (response.isSuccess)
                response.value = response.value.Where(x => x.CategoryName.ToLower().Contains(request.ToLower())).ToList();

            return Ok(response);
        }

        [HttpGet("GetUsername")]
        public async Task<IActionResult> GetUsername(string request)
        {
            var response = await _userService.GetList();
            if (response.isSuccess)
                response.value = response.value.Where(x => x.Username.ToLower().Contains(request.ToLower())).ToList();

            return Ok(response);
        }

        [HttpGet("GetPosition")]
        public async Task<IActionResult> select2Position(string request)
        {
            var response = await _positionService.GetList();
            if (response.isSuccess)
                response.value = response.value.Where(x => x.PositionName.ToLower().Contains(request.ToLower())).ToList();

            return Ok(response);
        }
    }
}
