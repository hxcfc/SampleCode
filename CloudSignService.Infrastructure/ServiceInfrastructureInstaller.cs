using CloudSignService.Application.Interfaces.Authorization;
using CloudSignService.Infrastructure.Implementations.Authorization;

namespace CloudSignService.Infrastructure
{
    public static class ServiceInfrastructureInstaller
    {
        public static IServiceCollection InstallInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IXyzServiceLogger, XyzServiceLogger>();
            services.AddSingleton<ILoggerArchivator, LoggerArchivator>();
            services.AddTransient<IAuthorization, Authorization>();

            return services;
        }
    }
}