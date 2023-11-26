using Microsoft.Extensions.DependencyInjection;
using SoftLineTestProj.Settings;
using System;
using SoftLineTestProj.Database;
using Microsoft.Extensions.Configuration;
using System.Runtime;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

var mainSettings = Settings.Load<MainSettings>("Main");
var swaggerSettings = Settings.Load<SwaggerSettings>("Swagger");
var settings = Settings.Load<DbSettings>("Database");

builder.AddAppLogger();

var services = builder.Services;
services.AddAppCors();
services.AddAppVersioning();
services.AddAppSwagger(mainSettings, swaggerSettings);
services.AddDbContext<MyDbContext>(opt=> opt.UseNpgsql(settings.ConnectionString));

services.AddRazorPages();
services.AddControllers().AddNewtonsoftJson();

services
    .AddMainSettings()
    .AddSwaggerSettings()
    .AddApiSpecialSettings()
    ;

var app = builder.Build();
app.UseAppSwagger();
DbInit.Execute(app.Services);

app.MapRazorPages();
app.MapControllers();
app.UseStaticFiles();

app.Run();