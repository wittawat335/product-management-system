using Ecommerce.Web.Models;

namespace Ecommerce.Web.Services.Interfaces
{
    public interface IProductService
    {
        Task<Response<List<Product>>> GetList(string url, Product filter);
        Task<List<Product>> Select2Product(string url, string query);
        Task<Product> Detail(string id, string action);
        Task<Response<Product>> Save(ProductViewModel model);
    }
}
