using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<Response<List<ProductDTO>>> GetList(ProductDTO filter = null);
    }
}
