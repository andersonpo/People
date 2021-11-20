using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace People.Infrastructure.Extensions.HealthChecks
{
    public class SelfHealthcheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new HealthCheckResult(
                HealthStatus.Healthy,
                description: "API up!"));
        }
    }
}
