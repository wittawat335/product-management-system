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

        public CategoryController(ICategoryService service)
        {
            _service = service;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _service.GetList());
        }

        [HttpGet("GetListActive")]
        public async Task<IActionResult> GetListActive()
        {
            var filter = new CategoryDTO();
            filter.Status = "A";
            return Ok(await _service.GetList(filter));
        }


        [HttpGet("GetCategory/{id}")]
        public async Task<IActionResult> GetCategory(string id)
        {
            return Ok(await _service.Get(id));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(CategoryDTO request)
        {
            return Ok(await _service.Add(request));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(CategoryDTO request)
        {
            return Ok(await _service.Update(request));
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _service.Delete(id));
        }
    }
}
