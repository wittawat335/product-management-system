using Ecommerce.Web.Models;
using Ecommerce.Web.Models.Category;
using Ecommerce.Web.Models.Product;

namespace Ecommerce.Web.Services.Interfaces
{
    public interface IProductService
    {
        Task<Response<List<Product>>> GetListProduct(Product filter);
        Task<Response<List<Category>>> GetListCategory();
        Task<Response<Product>> Save(Product model);
        Task<Response<List<Product>>> GetListShopping(int pageSize, int p);
    }
}
