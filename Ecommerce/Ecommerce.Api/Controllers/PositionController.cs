using Ecommerce.Core.DTOs;
using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PositionController : ControllerBase
    {
        private readonly IPositionService _service;

        public PositionController(IPositionService service)
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
            var filter = new PositionDTO();
            filter.Status = "A";
            return Ok(await _service.GetList(filter));
        }


        [HttpGet("Get/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _service.Get(id));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(PositionDTO request)
        {
            return Ok(await _service.Add(request));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(PositionDTO request)
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
