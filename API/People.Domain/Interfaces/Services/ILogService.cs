namespace People.Domain.Interfaces.Services
{
    public interface ILogService
    {
        void LogDebug(string message);
        void LogInformation(string message);
        void LogException(string message);
        void LogException(string message, Exception ex);
    }
}
