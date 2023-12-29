using Ecommerce.Api.Jobs;
using Ecommerce.Core.Helper;
using Ecommerce.Core.Services.Interfaces;
using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendMailController : ControllerBase
    {
        private readonly IEmailService _service;
        private readonly IBackgroundJobClient _jobClient;
        private readonly IRecurringJobManager _recurringJobManager;

        public SendMailController(IEmailService service, IBackgroundJobClient jobClient, IRecurringJobManager recurringJobManager)
        {
            _service = service;
            _jobClient = jobClient;
            _recurringJobManager = recurringJobManager;
        }

        [HttpPost]
        public async Task<IActionResult> SendEmail([FromBody] MailRequest request)
        {
            var response = await _service.SendEmailAsync(request);
            return Ok(response);
        }


        [HttpPost("CreateBackgroudJob")]
        public IActionResult CreateBackgroudJob()
        {
            //BackgroundJob.Enqueue(() => Console.WriteLine("BackgroudJob Job Triggered"));
            BackgroundJob.Enqueue<Job>(x => x.WriteLog("BackgroudJob Job Triggered"));// ตั้งให้ทำงานทันทีที่เรียกใช้
            return Ok();
        }

        [HttpPost("CreateScheduleJob")]
        public IActionResult CreateScheduleJob()
        {
            var scheduleDateTime = DateTime.UtcNow.AddSeconds(5);
            var dateTimeOffset = new DateTimeOffset(scheduleDateTime);
            //BackgroundJob.Schedule(() => Console.WriteLine("Scheduled Job Triggered"), dateTimeOffset);
            BackgroundJob.Schedule<Job>(x => x.WriteLog("Scheduled Job Triggered"), dateTimeOffset);//ตั้งให้ทำงานหลังจากเรียกใช้งาน ... กี่วิ

            return Ok();
        }

        [HttpPost("CreateContinueJob")]
        public IActionResult CreateContinueJob()
        {
            var scheduleDateTime = DateTime.UtcNow.AddSeconds(5);
            var dateTimeOffset = new DateTimeOffset(scheduleDateTime);
            var jobId = BackgroundJob.Schedule(() => Console.WriteLine("Scheduled Job 2 Triggered"), dateTimeOffset);

            var job2Id = BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Continuation Job 1 Triggered"));
            var job3Id = BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine("Continuation Job 2 Triggered"));

            return Ok();
        }

        [HttpPost("CreateRecurringJob")]
        public IActionResult CreateRecurringJob()
        {
            RecurringJob.AddOrUpdate("RecurringJob", () => Console.WriteLine("Recurring Job Triggered"), "* * * * *"); //ตั้งให้ทำงานทุกๆ ... นาที
            return Ok();
        }

        [HttpGet]
        [Route("Enqueue")]
        public ActionResult Enqueue()
        {
            var jobId = _jobClient.Enqueue(() => _service.SendEmail("Enqueue", DateTime.Now.ToLongTimeString()));
            _jobClient.Enqueue(jobId, () => Console.WriteLine($"Email sent Succesffully with Id {jobId}"));
            return Ok($"Enqueue {jobId}");
        }

        [HttpGet]
        [Route("Schedule")]
        public ActionResult Schedule()
        {
            var jobId = _jobClient.Schedule(() 
                => _service.SendEmail("Schedule", DateTime.Now.ToLongTimeString()), TimeSpan.FromMinutes(2));

            return Ok($"Schedule 2 minute");
        }

        [HttpGet]
        [Route("AddOrUpdate")]
        public ActionResult AddOrUpdate()
        {
            RecurringJob.AddOrUpdate(() => _service.SendEmail("AddOrUpdate", DateTime.Now.ToLongTimeString()), Cron.Daily);

            return Ok($"AddOrUpdate");
        }

        [HttpGet]
        [Route("ContinueJobWith")]
        public ActionResult ContinueJobWith()
        {
            var jobId = BackgroundJob.Enqueue(() => _service.SendEmail("Fire-and-Forget Job", DateTime.Now.ToLongTimeString()));
            BackgroundJob.ContinueJobWith(jobId, () => Console.WriteLine($"Email sent With {jobId}"));

            return Ok($"ContinueJobWith {jobId}");
        }
    }
}
