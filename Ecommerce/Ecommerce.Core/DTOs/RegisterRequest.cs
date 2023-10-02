using Ecommerce.Core.Common;

namespace Ecommerce.Core.DTOs
{
    public class RegisterRequest
    {
        public string userName { get; set; }
        public string password { get; set; }
        public string fullName { get; set; }
        public string email { get; set; }
        public string positionId { get; set; } = string.Empty;
        public string address { get; set; } = string.Empty;
        public string status { get; set; } = Constants.Status.Active;
        public DateTime createDate { get; set; } = DateTime.Now;
    }
}
