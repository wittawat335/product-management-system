using Ecommerce.Core.DTOs;
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

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var response = await _service.GetList();
            return Ok(response);
        }

        [HttpGet("GetListActive")]
        public async Task<IActionResult> GetListActive()
        {
            var filter = new CategoryDTO { Status = "A" };
            var response = await _service.GetList(filter);
            return Ok(response);
        }


        [HttpGet("GetCategory/{id}")]
        public async Task<IActionResult> GetCategory(string id)
        {
            var response = await _service.Get(id);
            return Ok(response);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CategoryDTO request)
        {
            var response = await _service.Add(request);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(CategoryDTO request)
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
