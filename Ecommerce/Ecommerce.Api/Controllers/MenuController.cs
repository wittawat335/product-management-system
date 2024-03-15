﻿using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _service;

        public MenuController(IMenuService service) => _service = service;

        [HttpGet("{PositionId}")]
        public async Task<IActionResult> Get(string PositionId)
        {
            var response = await _service.GetMenuByPositionId(PositionId);
            return Ok(response);
        }

        [HttpGet("GetListActive")]
        public async Task<IActionResult> GetListActive()
        {
            var response = await _service.GetListMenuActive();
            return Ok(response);
        }

        [HttpGet("GetListByPermission/{PositionId}")]
        public async Task<IActionResult> GetListByPermission(string PositionId)
        {
            var response = await _service.GetListByPermission(PositionId);
            return Ok(response);
        }
    }
}
