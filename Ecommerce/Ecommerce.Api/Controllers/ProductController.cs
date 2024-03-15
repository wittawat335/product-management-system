using Ecommerce.Core.DTOs.Product;
using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Authorize(Roles = "Developer,Administrator,Manager,Employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> Get() 
        {
            var response = await _service.GetList();
            return Ok(response); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.Get(id);
            return Ok(response);
        }

        [HttpPost("Search")]
        public async Task<IActionResult> Search(ProductDTO filter)
        {
            var response = await _service.GetList(filter);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(ProductDTO request)
        {
            var response = await _service.Insert(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(ProductDTO request)
        {
            var response = await _service.Update(request);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.Delete(id);
            return Ok(response);
        }
    }
}
