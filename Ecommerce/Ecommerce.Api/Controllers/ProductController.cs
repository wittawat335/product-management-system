using Ecommerce.Core.DTOs;
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

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList() 
        {
            var response = await _service.GetList();
            return Ok(response); 
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(ProductDTO filter)
        {
            var response = await _service.GetList(filter);
            return Ok(response);
        }

        [HttpGet("GetProduct/{id}")]
        public async Task<IActionResult> GetProduct(string id)
        {
            var response = await _service.Get(id);
            return Ok(response);
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> UploadFile(List<ProductDTO> request)
        {
            var response = await _service.Upload(request);
            return Ok(response);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(ProductDTO request)
        {
            var response = await _service.Add(request);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(ProductDTO request)
        {
            var response = await _service.Update(request);
            return Ok(response);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.Delete(id);
            return Ok(response);
        }
    }
}
