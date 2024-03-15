using Ecommerce.Web.Models;
using Ecommerce.Web.Models.Authen;
using Ecommerce.Web.Models.Permission;

namespace Ecommerce.Web.Services.Interfaces
{
    public interface IAuthenService
    {
        Task<Response<Session>> Login(Login request);
        Task GetPermission(string positionId);
        Task<Response<Register>> Register(Register request);
        string GetIp();
        void LogOut();
        void SetSessionValue(Session session);
        void SetPermissionToSession(List<Permission> permission);
    }
}
