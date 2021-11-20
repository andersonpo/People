using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace People.Infrastructure.Extensions.HealthChecks
{
    public static class HealthcheckExtensions
    {
        public static IServiceCollection AddCustomHealthchecks(this IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck<SelfHealthcheck>("Self")
                .AddCheck<DatabaseHealthcheck>("Database");

            //TODO Bug no aspnet 6.0 com Queryable
            /*
            services.AddHealthChecksUI(setup =>
            {
                setup.SetEvaluationTimeInSeconds(60);
                setup.MaximumHistoryEntriesPerEndpoint(60);
                
                try
                {
                    var urlConfig = Environment.GetEnvironmentVariable("ASPNETCORE_URLS");
                    if (!string.IsNullOrWhiteSpace(urlConfig))
                    {
                        var urls = urlConfig.Split(';');
                        var uris = urls.Select(url => Regex.Replace(url, @"^(?<scheme>https?):\/\/((\+)|(\*)|(0.0.0.0))(?=[\:\/]|$)", "${scheme}://localhost"))
                                        .Select(uri => new Uri(uri, UriKind.Absolute)).ToArray();
                        var httpEndpoint = uris.FirstOrDefault(uri => uri.Scheme == "http");
                        var httpsEndpoint = uris.FirstOrDefault(uri => uri.Scheme == "https");
                        if (httpEndpoint != null) // Create an HTTP healthcheck endpoint
                        {
                            setup.AddHealthCheckEndpoint("Infraestrutura HTTP", new UriBuilder(httpEndpoint.Scheme, httpEndpoint.Host, httpEndpoint.Port, "/healthz-all").ToString());
                        }
                        if (httpsEndpoint != null) // Create an HTTPS healthcheck endpoint
                        {
                            setup.AddHealthCheckEndpoint("Infraestrutura HTTPS", new UriBuilder(httpsEndpoint.Scheme, httpsEndpoint.Host, httpsEndpoint.Port, "/healthz-all").ToString());
                        }
                    }
                    else
                    {
                        setup.AddHealthCheckEndpoint("Infraestrutura", "/healthz-all");
                    }
                }
                catch { }
            }).AddInMemoryStorage();
            */

            return services;
        }

        public static IApplicationBuilder UseHealthChecks(this IApplicationBuilder app)
        {
            // este endpoint so valida ele mesmo (self)
            app.UseHealthChecks("/healthz",
                new HealthCheckOptions()
                {
                    Predicate = e => "self".Equals(e.Name.ToLower()),
                    ResponseWriter = async (context, report) =>
                    {
                        var result = new
                        {
                            status = report.Status.ToString(),
                            entries = report.Entries.Select(e => new
                            {
                                name = e.Key,
                                status = e.Value.Status.ToString()
                            })
                        };
                        await context.Response.WriteAsJsonAsync(result);
                    }
                });

            return app;
        }

        public static IApplicationBuilder UserHealthCheckUi(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/healthz-all", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            // Ativa o dashboard para a visualização da situação de cada Health Check
            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/monitor";
                //options.ApiPath = "/healthz-all";
                //options.UseRelativeApiPath = true;
            });

            return app;
        }
    }
}
