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

using (var context = new MyDbContext())
{
    context.InitDb();
}

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