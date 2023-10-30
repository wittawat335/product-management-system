using Ecommerce.Core.DTOs;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Authorize(Roles = "Developer,Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service)
        {
            _service = service;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(PermissionDTO model)
        {
            return Ok(await _service.Add(model));
        }

        [HttpPost("SavePermission")]
        public async Task<IActionResult> SavePermission(PermissionDTO model)
        {
            return Ok(await _service.SavePermission(model));
        }

        [HttpDelete("DeleteList/{positionId}")]
        public async Task<IActionResult> DeleteList(string positionId)
        {
            return Ok(await _service.DeleteListByPositionId(positionId));
        }
    }
}
