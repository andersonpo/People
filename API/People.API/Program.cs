using FluentValidation.AspNetCore;
using People.Domain.Interfaces.Services;
using People.Infrastructure.Extensions.Database;
using People.Infrastructure.Extensions.DependencyInjections;
using People.Infrastructure.Extensions.Documentation;
using People.Infrastructure.Extensions.HealthChecks;
using People.Infrastructure.Extensions.Logs;
using People.Infrastructure.Extensions.Middlewares;
using People.Infrastructure.Extensions.OptionsPattern;
using People.Infrastructure.Extensions.Perfomance;
using Serilog;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

Log.Logger = LogExtensions.CreateLogger(builder.Configuration);

builder.Services.AddControllers().AddFluentValidation(fvc =>
    fvc.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services
    .AddOptionsPattern(builder.Configuration)
    .AddDependencyInjection(builder.Configuration)
    .AddCustomMiddleware()
    .AddCompressHttpCalls()
    .AddJsonResponseConfiguration()
    //.AddVersioning()
    .AddSwaggerrAuthorize(false)
    .AddSwaggerDocumentation()
    .AddCustomHealthchecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    //app.UseSwaggerUiMultipleVersions(builder.Services.BuildServiceProvider().GetRequiredService<IApiVersionDescriptionProvider>());
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(x =>
    x.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseRouting();
app.UseMiddleware<LoggerMiddleware>();

app.UseHealthChecks()
    .UserHealthCheckUi();

var logService = builder.Services.BuildServiceProvider().GetService<ILogService>();
var db = builder.Services.BuildServiceProvider().GetService<IDbConnection>();
if (db != null && logService != null)
{
    app.DatabaseCreate(db, logService);
}

/*
builder.Host.ConfigureWebHost((IWebHostBuilder x)=>
{
    x.Configure((IApplicationBuilder z) =>
    {
        var aa = z.ApplicationServices.GetService<ILogService>();
        var aa2 = z.ApplicationServices.GetService<IDbConnection>();
        app.DatabaseCreate(aa2!, aa!);
    }
    );
});
*/

var isDev = app.Environment.IsDevelopment();
var isStaging = app.Environment.IsStaging();
var isProd = app.Environment.IsProduction();
var envName = app.Environment.EnvironmentName;

Log.Information($"ENVIRONMENT: {envName} - DEV: {isDev} - STAGING: {isStaging} - PROD: {isProd}");

app.Run();
