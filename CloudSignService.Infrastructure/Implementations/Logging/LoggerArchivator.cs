namespace CloudSignService.Infrastructure.Implementations.Logging
{
    public class LoggerArchivator : ILoggerArchivator
    {
        private const string loggerName = "LoggerArchivator";
        private readonly IXyzServiceLogger _logger;

        public LoggerArchivator(IXyzServiceLogger logger)
        {
            _logger = logger;
        }

        public Task<bool> ArchiveLogs()
        {
            try
            {
                var baseFolder = AppDomain.CurrentDomain.BaseDirectory;
                var archiveLogFolder = Path.Combine(baseFolder, "LogArchive");
                if (!Directory.Exists(archiveLogFolder))
                {
                    Directory.CreateDirectory(archiveLogFolder);
                }
                var archiveLogFolderDate = Path.Combine(baseFolder + "LogArchive", DateTime.Now.ToString("yyyy-MM-dd"));
                if (!Directory.Exists(archiveLogFolderDate))
                {
                    Directory.CreateDirectory(archiveLogFolderDate);
                }
                var logFolder = Path.GetFullPath(Path.Combine(baseFolder, "Logi"));
                _logger.LogInformation(loggerName, $"Started archiving files in  {logFolder}");
                var files = Directory.GetFiles(logFolder, "*", SearchOption.AllDirectories);

                using MemoryStream ms = new MemoryStream();
                using (ZipArchive archive = new ZipArchive(ms, ZipArchiveMode.Update))
                {
                    foreach (var file in files)
                    {
                        var filename = Path.GetFileName(file);
                        var fileDirectoryName = new DirectoryInfo(Path.GetDirectoryName(file)).Name;
                        var entryName = Path.Combine(fileDirectoryName, filename);
                        ZipArchiveEntry orderEntry = archive.CreateEntry(entryName);
                        using BinaryWriter writer = new BinaryWriter(orderEntry.Open());
                        writer.Write(File.ReadAllBytes(file));
                    }
                }
                var archiveName = $"LogArchive_{DateTime.Now:yyyy-MM-dd}_{DateTime.Now:hh_mm_ss}.zip";

                File.WriteAllBytes(Path.Combine(archiveLogFolderDate, archiveName), ms.ToArray());
                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(loggerName, $"Logs couldn't be archived. Error {ex}");
                return Task.FromResult(false);
            }
        }

        public Task DeleteLogs()
        {
            _logger.LogInformation(loggerName, $"Deleting logs....");
            var baseFolder = AppDomain.CurrentDomain.BaseDirectory;
            var logFolder = Path.GetFullPath(Path.Combine(baseFolder, "Logi"));
            var directory = new DirectoryInfo(logFolder);

            foreach (FileInfo file in directory.GetFiles())
            {
                file.Delete();
            }

            foreach (DirectoryInfo subDirectory in directory.GetDirectories())
            {
                subDirectory.Delete(true);
            }
            _logger.LogInformation(loggerName, $"Logs deleted at {DateTime.Now}");

            return Task.CompletedTask;
        }
    }
}