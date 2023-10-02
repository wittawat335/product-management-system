using Ecommerce.Web.Extenions.Class;
using Ecommerce.Web.Services.Interfaces;
using Ecommerce.Web.Services;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Ecommerce.Web.Extenions
{
    public static class ServiceExtenions
    {
        public static void SessionConfig(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(option =>
            {
                option.LoginPath = "/Authen/Login";
                option.ExpireTimeSpan = TimeSpan.FromMinutes(15);
            });

            services.ConfigureApplicationCookie(options =>
            {
                options.Cookie.Name = "AspNetCore.Identity.Application";
                options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
                options.SlidingExpiration = true;
            }); // test
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //Session
            services.AddDistributedMemoryCache();
            services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(15);
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
