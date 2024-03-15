using System;
namespace Ecommerce.Core.Helper
{
    public class JwtSettings
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }
        public int Timeout { get; set; }
    }
}
