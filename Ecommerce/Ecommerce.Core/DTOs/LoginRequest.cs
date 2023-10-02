using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Core.DTOs
{
    public class LoginRequest
    {
        public string userName { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public string ip { get; set; } = string.Empty;
    }
}
