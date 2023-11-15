using Ecommerce.Core.DTOs;
using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Authorize(Roles = "Developer,Administrator,Manager,Employee")]
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
            var response = await _service.GetList();
            return Ok(response);
        }

        [HttpGet("GetUser/{id}")]
        public async Task<IActionResult> GetUser(string id)
        {
            var response = await _service.Get(id);
            return Ok(response);
        }

        [HttpGet("select2UserName")]
        public async Task<IActionResult> select2UserName(string query)
        {
            var response = await _service.GetList();
            response.value = response.value.Where(x => x.Username.ToLower().Contains(query.ToLower())).ToList();
            return Ok(response);
        }

        [HttpGet("select2Position")]
        public async Task<IActionResult> select2Position(string query)
        {
            var response = await _service.GetList();
            response.value = response.value.Where(x => x.PositionName.ToLower().Contains(query.ToLower())).ToList();
            return Ok(response);
        }

        [HttpPost("GetList")]
        public async Task<IActionResult> GetList(UserDTO request)
        {
            var response = await _service.GetList(request);
            return Ok(response);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(UserDTO request)
        {
            var response = await _service.Add(request);
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(UserDTO request)
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
