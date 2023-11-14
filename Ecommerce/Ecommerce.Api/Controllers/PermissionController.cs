using Ecommerce.Core.DTOs;
using Ecommerce.Core.Services.Interfaces;
using Ecommerce.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace Ecommerce.Api.Controllers
{
    //[Authorize(Roles = "Developer,Administrator")]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _service;

        public PermissionController(IPermissionService service)
        {
            _service = service;
        }

        [HttpGet("GetList/{positionId}")]
        public async Task<IActionResult> GetList(string positionId)
        {
            var response = await _service.GetList(positionId);
            return Ok(response);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(PermissionDTO request)
        {
            var response = await _service.Add(request);
            return Ok(response);
        }

        [HttpPost("Save")]
        public async Task<IActionResult> SaveAsync(string id, List<DataPermissionJsonInsertList> request)
        {
            var response = await _service.SaveList(id, request);
            return Ok(response);
        }

        //[HttpPost("Save")]
        //public async Task<IActionResult> Save(string id)
        //{
        //    string session = HttpContext.Session.GetString("listPermission");
        //    var response = await _service.SaveList(id);
        //    return Ok(response);
        //}

        [HttpPost("SavePermission")]
        public async Task<IActionResult> SavePermission(PermissionDTO request)
        {
            return Ok(await _service.SavePermission(request));
        }

        [HttpDelete("DeleteList/{positionId}")]
        public async Task<IActionResult> DeleteList(string positionId)
        {
            var response = await _service.DeleteListByPositionId(positionId);
            return Ok(response);
        }
    }
}
