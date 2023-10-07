﻿using Ecommerce.Web.Models;
using Ecommerce.Web.Models.Authen;

namespace Ecommerce.Web.Services.Interfaces
{
    public interface IAuthenService
    {
        Task<Response<Session>> Login(Login request);
        Task<Response<Register>> Register(Register request);
        string GetIp();
        void LogOut();
        void SetSessionValue(Session session);
    }
}
