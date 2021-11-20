using People.Domain.Interfaces.Services;
using Serilog;

namespace People.Infrastructure.Extensions.Logs
{
    public class LogService : ILogService
    {
        private readonly ILogger logger = Log.ForContext<LogService>();

        public void LogException(string message)
        {
            logger.Error(message);
        }

        public void LogException(string message, Exception ex)
        {
            logger.Error(ex, message);
        }

        public void LogInformation(string message)
        {
            logger.Information(message);
        }

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }
    }
}
