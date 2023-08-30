namespace CloudSign.Api.Installers.Extensions
{
    public static class InstallServiceExtension
    {
        public static void ConfigureServicesInAssembly(this IApplicationBuilder app)
        {
            typeof(Program).Assembly.ExportedTypes
                .Where(x => typeof(IInstallerConfiguration).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IInstallerConfiguration>()
                .ToList()
                .ForEach(installer => installer.ConfigureServices(app));
        }

        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            typeof(Program).Assembly.ExportedTypes
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IInstaller>()
                .ToList()
                .ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}