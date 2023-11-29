using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<Response<List<CategoryDTO>>> GetList(CategoryDTO filter = null);
        Task<Response<CategoryDTO>> Get(string id);
        Task<Response<Category>> Insert(CategoryDTO model);
        Task<Response<Category>> Update(CategoryDTO model);
        Task<Response<Category>> Delete(string id);
        Task<string> CheckDupilcate(string id);
    }
}
