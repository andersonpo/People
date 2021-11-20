using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Core;

namespace People.Infrastructure.Extensions.Logs
{
    public static class LogExtensions
    {
        public static Logger CreateLogger(IConfiguration configuration)
        {
            return new LoggerConfiguration()
                .Enrich.FromLogContext()
                .Enrich.WithCorrelationIdHeader()
                .Enrich.WithEnvironmentUserName()
                .Enrich.WithMachineName()
                .Enrich.WithEnvironmentName()
                .Enrich.WithProperty("Application", "People.API")
                .Filter.ByExcluding(x => x.Properties.Any(p =>
                {
                    return
                        p.Value.ToString().ToLower().Contains("healthcheck") ||
                        p.Key.ToString().ToLower().Contains("healthcheck");
                })
                )
                .MinimumLevel.Debug()
                .WriteTo.Console(restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Debug)
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }
    }
}
