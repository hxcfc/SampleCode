namespace CloudSignService.Application.Interfaces.Logging
{
    public interface ILoggerArchivator
    {
        Task<bool> ArchiveLogs();

        Task DeleteLogs();
    }
}