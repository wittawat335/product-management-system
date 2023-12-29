using Ecommerce.Core.Services.Interfaces;
namespace Ecommerce.Core.Services
{
    public class JobSevice : IJobService
    {
        public void ContinuationJob()
        {
            Console.WriteLine("Hi from Continuation Job");
        }

        public void DelayedJob()
        {
            Console.WriteLine("Hi from Delayed Job");
        }

        public void FireAndForgot()
        {
            Console.WriteLine("Hi from Fire And Forgot Job");
        }

        public void RecurringJob()
        {
            Console.WriteLine("Hi from Recurring Job");
        }
    }
}
