using CloudSignService.Application;
using CloudSignService.Infrastructure;
using CloudSignService.Persistance;

namespace CloudSign.Api.Installers.InstallServices
{
    public class DllInstaller : IInstaller
    {

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.InstallApplicationServices(configuration);
            services.InstallInfrastructureServices(configuration);
            services.InstallPersistanceServices(configuration);
        }
    }
}
