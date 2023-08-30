namespace CloudSign.Api.Installers.InstallServices.Swagger
{
    public static class SwaggerExtensions
    {
        public static IApplicationBuilder UseSwaggerAuthorized(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<SwaggerBasicAuthMiddleware>();
        }
    }
}