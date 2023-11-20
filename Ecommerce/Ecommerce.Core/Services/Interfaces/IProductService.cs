using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<Response<List<ProductDTO>>> GetList(ProductDTO request = null);
        Task<Response<ProductDTO>> Get(string id);
        Task<Response<Product>> Add(ProductDTO request);
        Task<Response<Product>> Update(ProductDTO request);
        Task<Response<Product>> Delete(string id);
        Task<Response<Product>> Upload(List<ProductDTO> request);
        Task<string> CheckDupilcate(string id, string name, string categoryId);
    }
}
