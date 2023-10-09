﻿using Ecommerce.Core.DTOs;
using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _service;

        public CategoryController(ICategoryService service)
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
            var filter = new CategoryDTO();
            filter.Status = "A";
            return Ok(await _service.GetList(filter));
        }
    }
}