using Ecommerce.Core.DTOs;
using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }

        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
            return Ok(await _service.GetList());
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            return Ok(await _service.Get(id));
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(UserDTO request)
        {
            return Ok(await _service.Add(request));
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UserDTO request)
        {
            return Ok(await _service.Update(request));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _service.Delete(id));
        }
    }
}
