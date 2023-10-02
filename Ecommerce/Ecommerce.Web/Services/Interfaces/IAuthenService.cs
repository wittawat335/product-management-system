using Ecommerce.Web.Models;

namespace Ecommerce.Web.Services.Interfaces
{
    public interface IAuthenService
    {
        Task<Response<Session>> Login(Login request);
        string GetIp();
        void LogOut();
        void SetSessionValue(Session session);
    }
}
