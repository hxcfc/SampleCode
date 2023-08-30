namespace CloudSignService.Infrastructure.Implementations.Logging
{
    public class XyzServiceLogger : IXyzServiceLogger
    {
        private readonly ILogger<XyzServiceLogger> logger;

        public XyzServiceLogger(ILogger<XyzServiceLogger> logger)
        {
            this.logger = logger;
        }

        public void LogError(string loggerName, string message, Exception? exception = null)
        {
            LogManager.Configuration.Variables["loggerType"] = loggerName;
            logger.LogError(message);
        }

        public void LogInformation(string loggerName, string message)
        {
            LogManager.Configuration.Variables["loggerType"] = loggerName;
            logger.LogInformation(message);
        }
    }
}