using System.Net;

namespace Ecommerce.Web.Models
{
    public class Response<T>
    {
        public T value { get; set; }
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public int statusCode { get; set; }
        public string returnUrl { get; set; }
    }
}
