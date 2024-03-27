using Ecommerce.Web.Services.Interfaces;
using Ecommerce.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Ecommerce.Web.Extenions
{
    public static class ServiceExtenions
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
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(sessionTimeOut);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.Name = cookieName;
            });
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient(typeof(IBaseApiService<>), typeof(BaseApiService<>));
            services.AddScoped<ICommonService, CommonService>();
            services.AddScoped<IAuthenService, AuthenService>();
        }
    }
}
