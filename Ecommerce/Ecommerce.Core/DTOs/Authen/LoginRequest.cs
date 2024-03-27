using System;

namespace Ecommerce.Core.DTOs.Authen
{
    public class LoginRequest
    {
        public string username { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string ip { get; set; } = string.Empty;
    }
}
