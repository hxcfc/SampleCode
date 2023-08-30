using Microsoft.AspNetCore.Mvc.Formatters;

namespace CloudSign.Api.Installers.InstallServices
{
    public class ControllersInstaller : IInstaller, IInstallerConfiguration
    {
        public void ConfigureServices(IApplicationBuilder app)
        {
        }

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<CasOptions>(configuration.GetSection("CasOptions"));
            services.AddControllers(options =>
            {
                options.RespectBrowserAcceptHeader = true;
                options.InputFormatters.Add(new XmlSerializerInputFormatter(options));
                options.OutputFormatters.Add(new XmlSerializerOutputFormatter());
            });
            services.AddMvc(c =>
            {
                c.Conventions.Add(new ApiExplorerGroupPerApiConvention());
            }).AddXmlSerializerFormatters();
            services.AddControllers(options =>
            {
                options.Filters.Add(new ApiExceptionFilter());
            });

            services.AddEndpointsApiExplorer();
        }
    }
}