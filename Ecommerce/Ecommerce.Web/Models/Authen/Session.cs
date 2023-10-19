namespace Ecommerce.Web.Models.Authen
{
    public class Session
    {
        public string userId { get; set; } = string.Empty;
        public string userName { get; set; } = string.Empty;
        public string fullName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string positionId { get; set; } = string.Empty;
        public string positionName { get; set; } = string.Empty;
        public string token { get; set; } = string.Empty;
    }
}
