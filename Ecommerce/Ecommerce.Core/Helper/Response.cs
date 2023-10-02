
namespace Ecommerce.Core.Helper
{
    public class Response<T>
    {
        public T value { get; set; }
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public string returnUrl { get; set; }
    }
}
