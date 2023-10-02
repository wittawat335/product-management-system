using Ecommerce.Api.Extentions;
using Ecommerce.Core;
using Ecommerce.Core.Common;
using Ecommerce.Infrastructure;

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

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(cor);
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
