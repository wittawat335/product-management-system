using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service)
        {
            _service = service;
        }

        [HttpGet("{PositionId}")]
        public async Task<IActionResult> GetList(string PositionId)
        {
            return Ok(await _service.GetMenuByPositionId(PositionId));
        }

        [HttpGet("GetListActive")]
        public async Task<IActionResult> GetListActive()
        {
            return Ok(await _service.GetListMenuActive());
        }
    }
}
