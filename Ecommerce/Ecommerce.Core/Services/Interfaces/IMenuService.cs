using Ecommerce.Core.DTOs;
using Ecommerce.Core.Helper;

namespace Ecommerce.Core.Services.Interfaces
{
    public interface IMenuService
    {
        Task<Response<List<MenuDTO>>> GetListMenu(string userId);
        Task<Response<List<MenuDTO>>> GetMenuByPositionId(string positionId);
        Response<List<MenuDTO>> GetListMenuExists(string positionId);
    }
}
