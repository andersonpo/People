using Microsoft.Extensions.DependencyInjection;

namespace People.Infrastructure.Extensions.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IServiceCollection AddCustomMiddleware(this IServiceCollection services)
        {
            services.AddTransient<LoggerMiddleware>();
            return services;
        }
    }
}
