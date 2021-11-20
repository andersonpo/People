using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace People.Infrastructure.Extensions.HealthChecks
{
    public class DatabaseHealthcheck : IHealthCheck
    {
        const string DatabaseDescription = "Banco de Dados";

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy, description: DatabaseDescription));
            /*
            try
            {
                dapperDataContext.DbConnection.Open();
                dapperDataContext.DbConnection.Close();
                return Task.FromResult(new HealthCheckResult(HealthStatus.Healthy, description: DatabaseDescription));
            }
            catch
            {
                return Task.FromResult(new HealthCheckResult(HealthStatus.Unhealthy, description: DatabaseDescription));
            }
            */
        }
    }
}
