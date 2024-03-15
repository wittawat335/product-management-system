using Ecommerce.Web.Extenions.Class;

namespace Ecommerce.Web.Extenions
{
    public static class AppSettingExtenion
    {
        public static void AddAppSetting(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AppSetting>(configuration.GetSection("AppSettings"));
        }
    }
}
