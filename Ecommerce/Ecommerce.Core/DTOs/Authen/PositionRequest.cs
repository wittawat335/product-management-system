using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs.Authen
{
    public class PositionRequest
    {
        public string positionName { get; set; }
        public string status { get; set; }
        public DateTime createDate { get; set; } = DateTime.Now;
    }
}
