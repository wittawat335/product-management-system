using Ecommerce.Core.DTOs;
using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Authorize(Roles = "Developer,Administrator,Manager,Employee")]
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _service;

        public PositionController(IPositionService service)
        {
            _service = service;
        }

        [Authorize(Roles = "Developer,Administrator")]
        [HttpGet("GetJsTree/{positionId}")]
        public async Task<IActionResult> GetJsTree(string positionId)
        {
            var response = await _service.GetListPermissionData(positionId);
            return Ok(response);
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            var response = await _service.GetList();
            return Ok(response);
        }

        [HttpGet("GetListActive")]
        public async Task<IActionResult> GetListActive()
        {
            var filter = new PositionDTO { Status = "A" };
            var response = await _service.GetList(filter);
            return Ok(response);
        }

        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _service.Get(id);
            return Ok(response);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(PositionDTO request)
        {
            var response = await _service.Add(request);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(PositionDTO request)
        {
            var response = await _service.Update(request);
            return Ok(response);
        }

        [Authorize(Roles = "Developer")]
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _service.Delete(id);
            return Ok(response);
        }
    }
}
