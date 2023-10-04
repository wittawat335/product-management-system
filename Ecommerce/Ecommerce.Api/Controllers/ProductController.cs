using Ecommerce.Core.DTOs;
using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _service.GetList());
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(ProductDTO filter)
        {
            return Ok(await _service.GetList(filter));
        }
    }
}
