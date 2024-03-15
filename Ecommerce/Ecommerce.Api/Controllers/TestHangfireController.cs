using Ecommerce.Core.Services.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestHangfireController : ControllerBase
    {
        private readonly IJobService _service;
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        public TestHangfireController(IJobService service, IBackgroundJobClient backgroundJobClient, IRecurringJobManager recurringJobManager)
        {
            _service = service;
            _backgroundJobClient = backgroundJobClient;
            _recurringJobManager = recurringJobManager;
        }

        [HttpGet("/FireAndForgetJob")]
        public ActionResult CreateFireAndForgetJob()
        {
            _backgroundJobClient.Enqueue(() => _service.FireAndForgot());
            return Ok();
        }

        [HttpGet("/DelayedJob")]
        public ActionResult CreateDelayedJob()
        {
            _backgroundJobClient.Schedule(() => _service.DelayedJob(), TimeSpan.FromSeconds(30));
            return Ok();
        }

        [HttpGet("/RecurringJob")]
        public ActionResult CreateRecurringJob()
        {
            _recurringJobManager.AddOrUpdate("JobId", () => _service.RecurringJob(), Cron.Minutely);
            return Ok();
        }

        [HttpGet("/ContinuationJob")]
        public ActionResult CreateContinuationJob()
        {
            var parentJobId = _backgroundJobClient.Enqueue(() => _service.FireAndForgot());
            _backgroundJobClient.ContinueJobWith(parentJobId, () => _service.ContinuationJob());

            return Ok();
        }
    }
}
