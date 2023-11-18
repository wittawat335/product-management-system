using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly IEmailService _service;

        public SendMailController(IEmailService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] MailRequest request)
        {
            var response = await _service.SendEmailAsync(request);
            return Ok(response);
        }
    }
}
