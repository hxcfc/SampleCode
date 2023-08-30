namespace CloudSign.Api.Middleware.Diagnostic
{
    public static class DiagnosticMiddlewareExtension
    {
        public static IApplicationBuilder UseExtendedConsoleDiagnostic(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestDiagnosticMiddleware>();
        }
    }
}