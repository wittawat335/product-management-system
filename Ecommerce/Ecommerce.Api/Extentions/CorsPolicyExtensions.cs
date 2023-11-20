using Ecommerce.Core.Common;

namespace Ecommerce.Api.Extentions
{
    public static class CorsPolicyExtensions
    {
        public static void ConfigureCorsPolicy(this IServiceCollection services, IConfiguration configuration)
        {
            var url = configuration[Constants.AppSettings.Client_URL].ToString();
            var CorsPolicy = configuration[Constants.AppSettings.CorsPolicy].ToString();

            var blazorUrl = configuration["AppSettings:Blazor_URL"].ToString();
            var blazorCors = configuration["AppSettings:BlazorCors"].ToString();
            services.AddCors(opt =>
            {
                opt.AddPolicy(CorsPolicy, builder =>
                {
                    builder.WithOrigins(url).AllowAnyHeader().AllowAnyMethod();
                });

                opt.AddPolicy(blazorCors, builder =>
                {
                    builder.WithOrigins(blazorUrl).AllowAnyHeader().AllowAnyMethod();
                });
            });

            ////blazor
            //services.AddCors(opt =>
            //{
            //    opt.AddPolicy(blazorCors, builder =>
            //    {
            //        builder.WithOrigins(blazorUrl).AllowAnyHeader().AllowAnyMethod();
            //    });
            //});
        }
    }
}
