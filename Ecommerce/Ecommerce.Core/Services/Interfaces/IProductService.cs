using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IProductService
    {
        Response<List<ProductDTO>> GetList();
    }
}
