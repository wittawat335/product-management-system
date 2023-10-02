using Ecommerce.Core.DTOs;
using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenController : ControllerBase
    {
        private readonly IAuthenService _service;
        private readonly IWebHostEnvironment _environment;
        public AuthenController(IAuthenService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        [Route("CheckEnvironment")]
        public IActionResult CheckEnvironment()
        {
            return Content("Environment : " + _environment.EnvironmentName);
        }


        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            return Ok(await _service.Login(request));
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            return Ok(await _service.Register(request));
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("AddPosition")]
        public async Task<IActionResult> AddPosition([FromBody] PositionRequest request)
        {
            return Ok(await _service.AddPosition(request));
        }
    }
}
