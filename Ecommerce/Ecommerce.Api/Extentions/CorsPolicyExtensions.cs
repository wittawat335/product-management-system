using Ecommerce.Core.Common;

namespace Ecommerce.Api.Extentions
{
    public static class CorsPolicyExtensions
    {
        public static void ConfigureCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration[Constants.AppSettings.Client_URL].ToString();
            var CorsPolicy = configuration[Constants.AppSettings.CorsPolicy].ToString();
            services.AddCors(opt =>
            {
                opt.AddPolicy(CorsPolicy, builder =>
                {
                    builder.WithOrigins(url).AllowAnyHeader().AllowAnyMethod();
                });
            });

            //services.AddCors(opt =>
            //{
            //    opt.AddPolicy("AllowOrigin", builder =>
            //    {
            //        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
            //    });
            //});
        }
    }
}
