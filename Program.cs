using Microsoft.Extensions.DependencyInjection;
using SoftLineTestProj.Settings;

var builder = WebApplication.CreateBuilder(args);

var mainSettings = Settings.Load<MainSettings>("Main");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");

builder.AddAppLogger();

var services = builder.Services;
services.AddAppCors();
services.AddAppVersioning();
services.AddAppSwagger(mainSettings, swaggerSettings);
services.AddRazorPages();
services.AddControllers().AddNewtonsoftJson();

services
    .AddMainSettings()
    .AddSwaggerSettings()
    .AddApiSpecialSettings()
    ;

var app = builder.Build();
// Configure the HTTP request pipeline.
app.UseAppSwagger();
app.MapRazorPages();
app.MapControllers();

app.Run();