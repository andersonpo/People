using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using People.Domain.Configurations;

namespace People.Infrastructure.Extensions.OptionsPattern
{    public static class OptionsExtensions
    {
        public static IServiceCollection AddOptionsPattern(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DataBaseConfigOptions>(configuration.GetSection(DataBaseConfigOptions.NameConfig));
            services.Configure<BaseConfigOptions>(configuration.GetSection(BaseConfigOptions.NameConfig));

            return services;
        }
    }
}
