using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IUserService
    {
        Task<Response<List<UserDTO>>> GetList(UserDTO filter = null);
        Task<Response<UserDTO>> Get(string id);
        Task<Response<User>> Add(UserDTO model);
        Task<Response<User>> Update(UserDTO model);
        Task<Response<User>> Delete(string id);
    }
}
