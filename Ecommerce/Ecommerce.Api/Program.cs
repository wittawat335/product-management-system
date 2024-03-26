using Ecommerce.Api.Extentions;
using Ecommerce.Core;
using Ecommerce.Core.Common;
using Ecommerce.Core.Middlewares;
using Ecommerce.Infrastructure;
using Hangfire;
using Hangfire.MemoryStorage;
using HangfireBasicAuthenticationFilter;

var builder = WebApplication.CreateBuilder(args);
var cor = builder.Configuration[Constants.AppSettings.CorsPolicy].ToString();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration); //DBsettting
builder.Services.AddAppSetting(builder.Configuration);
builder.Services.AddService();
builder.Services.AuthenticationConfig(builder.Configuration);
builder.Services.ConfigureCorsPolicy(builder.Configuration); // addCors

builder.Services.AddTransient<ExceptionMiddleware>();

builder.Services.AddHangfire(x => x.UseMemoryStorage());
builder.Services.AddHangfireServer();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseStaticFiles();
app.UseCors(cor);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.CongigureExceptionMiddleware();
app.UseHangfireDashboard("/HangFire/Dashboard", new DashboardOptions
{
    DashboardTitle = "Hangfire Job",
    DarkModeEnabled = false,
    DisplayStorageConnectionString = false,
    Authorization = new[]
    {
        new HangfireCustomBasicAuthenticationFilter{ User = "admin", Pass = "12345"}
    }
});
app.Run();
