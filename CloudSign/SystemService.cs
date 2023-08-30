namespace CloudSign.Api
{
    public class SystemService
    {
        private readonly Logger logger;

        public SystemService()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public void Start(string[] args)
        {
            InfoScreen();

            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddCors();

            builder.Services.InstallServicesInAssembly(builder.Configuration);

            builder.Host.UseNLog();

            var app = builder.Build();
            app.UseCors(x => x
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true)
                .AllowCredentials());

            if (app.Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            };

            app.UseStaticFiles();
            app.UseDefaultFiles();

            app.UseHttpsRedirection();
            app.UseRouting();

            app.MapControllers();
            app.MapSwagger();

            app.UseMetricServer();

            app.ConfigureServicesInAssembly();

            app.UseAuthentication();
            app.UseAuthorization();
            Task.Run(() => app.Run());
            logger.Info($"Started Successfull!");
        }

        public void Stop()
        {
            logger.Info($"Application stopped at {DateTime.Now:yyyy-MM-dd hh:mm:ss}");
        }

        private void InfoScreen()
        {
            logger.Info("-------------------------------------------------------------------------------------------------------");
            logger.Info(string.Empty);
            logger.Info($"{CloudSignServiceApiVersion.Name} {CloudSignServiceApiVersion.Version}");
            logger.Info(string.Empty);
            logger.Info("-------------------------------------------------------------------------------------------------------");
        }
    }
}