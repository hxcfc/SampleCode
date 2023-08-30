namespace CloudSignService.Application
{
    public static class ServiceApplicationInstaller
    {
        public static IServiceCollection InstallApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                cfg.AddOpenBehavior(typeof(ValidationBehaviour<,>));
            });

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }
    }
}