using Ecommerce.Web.Models;

namespace Ecommerce.Web.Services.Interfaces
{
    public interface IProductService
    {
        Task<Response<List<Product>>> GetListProduct(Product filter);
        Task<Response<List<Category>>> GetListCategory();
        Task<List<Product>> Select2Product(string query);
        Task<List<Category>> Select2Category(string query);
        Task<Product> Detail(string id, string action);
        Task<Response<Product>> Save(Product model);
        Task<Response<List<Product>>> GetListShopping(int pageSize, int p);
    }
}
