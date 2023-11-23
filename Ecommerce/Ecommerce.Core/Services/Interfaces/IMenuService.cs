using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IMenuService
    {
        Task<Response<List<MenuDTO>>> GetListMenuActive();
        Task<Response<List<MenuDTO>>> GetMenuByPositionId(string positionId);
        Task<Response<List<MenuDTO>>> GetListByPermission(string positionId);
    }
}
