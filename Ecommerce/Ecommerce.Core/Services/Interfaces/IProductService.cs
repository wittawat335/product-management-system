using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IProductService
    {
        Task<Response<List<ProductDTO>>> GetList(ProductDTO filter = null);
        //Task<Response<string>> DBMultiUploadImage(IFormFileCollection filecollection, string productId);
        //Task<Response<string>> GetDBMultiImage(string productId);
        //Task<Response<string>> dbdownload(string productId);
    }
}
