using System;

namespace Ecommerce.Core.DTOs.Authen
{
    public class LoginRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string ip { get; set; } = string.Empty;
    }
}
