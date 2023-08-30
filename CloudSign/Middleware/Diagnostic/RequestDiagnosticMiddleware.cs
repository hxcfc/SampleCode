namespace CloudSign.Api.Middleware.Diagnostic
{
    public class RequestDiagnosticMiddleware
    {
        private const string loggerName = "RequestDiagnosticMiddleware";
        private readonly IXyzServiceLogger _logger;
        private readonly RequestDelegate next;

        public RequestDiagnosticMiddleware(RequestDelegate next, IXyzServiceLogger logger)
        {
            this.next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Request.EnableBuffering();
            var originalBodyStream = context.Response.Body;
            await using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;
            await PrintRequestOnConsole(context);
            await next(context);
            await PrintResponseOnConsole(context);
            await responseBody.CopyToAsync(originalBodyStream);
        }

        private string GetOriginBody(HttpContext context)
        {
            try
            {
                context.Request.Body.Seek(0, SeekOrigin.Begin);
                var sr = new StreamReader(context.Request.Body, Encoding.UTF8);

                var readResult = sr.ReadToEndAsync().GetAwaiter().GetResult();
                if (readResult != null)
                {
                    context.Request.Body.Seek(0, SeekOrigin.Begin);
                    return readResult;
                }

                throw new Exception(ST.RequestBodyReadFail);
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
                throw new Exception(ST.RequestBodyReadFail);
            }
        }

        private async Task PrintRequestOnConsole(HttpContext context)
        {
            try
            {
                var request = context.Request;

                _logger.LogInformation(loggerName, ST.RequestIncoming(DateTime.Now));

                _logger.LogInformation(loggerName, ST.MthodPath(request.Method, request.Path));
                _logger.LogInformation(loggerName, ST.QueryString(request.QueryString));
                _logger.LogInformation(loggerName, ST.HEADERS);

                request.Headers.ToList().ForEach(x =>
                    _logger.LogInformation(loggerName, ST.KeyValue(x.Key, x.Value)));

                _logger.LogInformation(loggerName, ST.BODY);

                var requestContent = GetOriginBody(context);
                _logger.LogInformation(loggerName, requestContent);

                _logger.LogInformation(loggerName, ST.ENDREQUEST);
                request.Body.Position = 0;
            }
            catch (Exception ex)
            {
                Console.Write(ex.Message);
            }
        }

        private async Task PrintResponseOnConsole(HttpContext context)
        {
            var response = context.Response;
            _logger.LogInformation(loggerName, ST.RESPONSE);
            _logger.LogInformation(loggerName, ST.HEADERS);

            response.Headers.ToList().ForEach(x => _logger.LogInformation(loggerName, ST.KeyValue(x.Key, x.Value)));

            _logger.LogInformation(loggerName, ST.BODY);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var text = await new StreamReader(context.Response.Body).ReadToEndAsync();
            _logger.LogInformation(loggerName, text);
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            _logger.LogInformation(loggerName, ST.ENDRESPONSE);
        }
    }
}