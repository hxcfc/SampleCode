namespace CloudSignService.Application.Interfaces.Logging
{
    public interface IXyzServiceLogger
    {
        void LogError(string loggerName, string message, Exception? exception = null);

        void LogInformation(string loggerName, string message);
    }
}