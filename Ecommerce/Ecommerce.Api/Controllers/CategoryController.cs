using Ecommerce.Core.DTOs.Category;
using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Authorize(Roles = "Developer,Administrator,Manager,Employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;
        public CategoryController(ICategoryService service) => _service = service;

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

        [HttpPost]
        public async Task<IActionResult> Insert(CategoryDTO request) 
        {
            var response = await _service.Insert(request);
            return Ok(response); 
        }

        [HttpPut]
        public async Task<IActionResult> Update(CategoryDTO request) 
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
