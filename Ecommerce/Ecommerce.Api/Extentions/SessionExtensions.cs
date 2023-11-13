using Microsoft.AspNetCore.Authentication.Cookies;

namespace Ecommerce.Api.Extentions
{
    public static class SessionExtensions
    {
        public static void SessionConfig(this IServiceCollection services, IConfiguration configuration)
        {
            var sessionTimeOut = configuration.GetValue<int>("AppSettings:SessionTimeOut");
            var cookieName = configuration.GetValue<string>("AppSettings:CookieName");
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.LoginPath = "/Authen/Login";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(sessionTimeOut);
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Session
            services.AddDistributedMemoryCache();
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(sessionTimeOut);
                option.Cookie.Name = cookieName;
            });
        }
    }
}
