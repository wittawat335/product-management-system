using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IJobService
    {
        void FireAndForgot();
        void RecurringJob();
        void DelayedJob();
        void ContinuationJob();
    }
}
