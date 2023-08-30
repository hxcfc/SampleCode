using CloudSignService.Application.Contracts.Repositories;
using CloudSignService.Persistance.Repositories;
using Common.Options;

namespace CloudSignService.Persistance
{
    public static class ServicePersistanceInstaller
    {
        public static IServiceCollection InstallPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<MongoDatabaseOptions>(configuration.GetSection("MongoDb"));

            services.AddScoped<IMongoDbRepository, MongoDbRepository>();

            return services;
        }
    }
}