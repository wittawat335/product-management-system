using Ecommerce.Core.DTOs;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    //[Authorize(Roles = "Developer,Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service) => _service = service;

        [HttpGet("{positionId}")]
        public async Task<IActionResult> GetList(string positionId)
        {
            var response = await _service.GetList(positionId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(string id, List<DataPermissionJsonInsertList> request)
        {
            var response = await _service.SaveList(id, request);
            return Ok(response);
        }
    }
}
