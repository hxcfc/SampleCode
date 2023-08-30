namespace CloudSign.Api.Installers.InstallServices.Swagger
{
    public class SwaggerInstaller : IInstaller, IInstallerConfiguration
    {
        public void ConfigureServices(IApplicationBuilder app)
        {
            app.UseSwaggerAuthorized();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.RoutePrefix = "cloudsignswagger";
                c.EnableFilter();
                c.DocExpansion(DocExpansion.List);
                c.DefaultModelsExpandDepth(-1);
                c.DefaultModelRendering(ModelRendering.Model);
                string swaggerPath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
                c.SwaggerEndpoint($"{swaggerPath}/swagger/cloudsignservice/swagger.json", "Cloud Sign Service V1");
                c.SwaggerEndpoint($"{swaggerPath}/swagger/diagnostic/swagger.json", "Diagnostic");
            });
        }

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(options =>
            {
                var contact = new OpenApiContact()
                {
                    Email = "dps@XyzService.pl",
                    Name = "DPS",
                };
                options.SwaggerDoc("cloudsignservice", new OpenApiInfo
                {
                    Version = "V1",
                    Title = $"Cloud Sign Service API V1",
                    Contact = contact,
                    Description = "Cloud Sign Service Usługa do obsługi komunikacji",
                });
                options.SwaggerDoc("diagnostic", new OpenApiInfo
                {
                    Version = "diagnostic",
                    Title = $"Cloud Sign Service Diagnostic API",
                    Contact = contact,
                    Description = "Endpointy diagnostyczne"
                });
                options.EnableAnnotations();
                options.IgnoreObsoleteActions();
                options.IgnoreObsoleteProperties();
                options.CustomSchemaIds(type => $"{type.FullName}");
                options.TagActionsBy(api =>
                {
                    if (api.GroupName != null && api.GroupName != "cloudsignservice")
                    {
                        return new[] { api.GroupName };
                    }

                    if (api.ActionDescriptor is ControllerActionDescriptor controllerActionDescriptor)
                    {
                        return new[] { controllerActionDescriptor.ControllerName };
                    }

                    throw new InvalidOperationException("Unable to determine tag for endpoint.");
                });

                var jwtSecurityScheme = new OpenApiSecurityScheme
                {
                    BearerFormat = "JWT",
                    Name = "JWT Authentication",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    Description = "Token JWT do zabezpieczenia komunikacji",

                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };

                options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtSecurityScheme, Array.Empty<string>() }
                });
            });
        }
    }
}