using Ecommerce.Core.Common;

namespace Ecommerce.Core.Helper
{
    public class Response<T>
    {
        public T value { get; set; }
        public bool isSuccess { get; set; } = false;
        public string message { get; set; } = Constants.StatusMessage.No_Data;
        public int statusCode { get; set; }
        public string returnUrl { get; set; }
    }
}
