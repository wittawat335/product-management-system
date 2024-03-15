using Ecommerce.Core.DTOs.Authen;
using Ecommerce.Core.Helper;
using Ecommerce.Domain.Entities;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IAuthenService
    {
        Task<Response<LoginResponse>> Login(LoginRequest request);
        Task<Response<string>> Register(RegisterRequest request);
        Task<Response<LoginResponse>> GenerateToken(User user);
    }
}
