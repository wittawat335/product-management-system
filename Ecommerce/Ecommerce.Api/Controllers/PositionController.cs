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

        public PositionController(IPositionService service) => _service = service;


        [Authorize(Roles = "Developer,Administrator")]
        [HttpGet("{positionId}")]
        public async Task<IActionResult> GetJsTree(string positionId)
        {
            var response = await _service.GetListPermissionData(positionId);
            return Ok(response);
        }

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
        public async Task<IActionResult> Insert(PositionDTO request)
        {
            var response = await _service.Insert(request);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update(PositionDTO request)
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
