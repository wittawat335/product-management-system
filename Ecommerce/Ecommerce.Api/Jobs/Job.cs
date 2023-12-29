namespace Ecommerce.Api.Jobs
{
    public class Job
    {
        private readonly ILogger _logger;
        public Job(ILogger<Job> logger) => _logger = logger;
        public void WriteLog(string message)
        {
            _logger.LogInformation($"{DateTime.Now:yyyy-MM-dd hh:mm:ss tt} {message}");
        }
    }
}
