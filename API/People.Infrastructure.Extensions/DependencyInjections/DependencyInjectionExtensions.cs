using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using People.Domain.Interfaces.Repositories;
using People.Domain.Interfaces.Services;
using People.Infrastructure.Extensions.Logs;
using People.Services.Services;
using People.Services.Repositories;
using System.Data;
using System.Data.SqlClient;
using People.Domain.Notifications;
using People.Domain.Interfaces;

namespace People.Infrastructure.Extensions.DependencyInjections
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            // Log
            services.AddTransient<ILogService, LogService>();
            services.AddScoped<IApiNotification, ApiNotification>();

            // Database
            var SqlServerConnectionString = configuration.GetConnectionString("SqlServer");
            services.AddTransient<IDbConnection>((sp) => new SqlConnection(SqlServerConnectionString));

            // Services
            services.AddScoped<ITechnologyService, TechnologyService>();


            // Repositories
            services.AddScoped<ITechnologyRepository, TechnologyRepository>();

            return services;
        }
    }
}
