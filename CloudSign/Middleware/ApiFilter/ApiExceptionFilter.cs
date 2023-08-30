namespace CloudSign.Api.Middleware.ApiFilter
{
    public class ApiExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, Action<ExceptionContext>> exceptionHandlers;
        private readonly NLog.ILogger logger = LogManager.GetCurrentClassLogger();
        private readonly JsonSerializerOptions options;

        public ApiExceptionFilter()
        {
            options = new JsonSerializerOptions()
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            exceptionHandlers = new Dictionary<Type, Action<ExceptionContext>>
            {
                { typeof(Exception), HandleException },
                { typeof(NotFoundException), HandleNotFoundException },
                { typeof(DbBadRequestException), HandleCodeException },
                { typeof(InMethodException), HandleCodeException },
                { typeof(ForbiddenException), HandleForbiddenException },
                { typeof(ShortInMethodException), HandleCodeException },
                { typeof(CodeException), HandleCodeException },
                { typeof(BadRequest), HandleException },
                { typeof(FluentValidation.ValidationException), HandValidationleException },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (exceptionHandlers.ContainsKey(type))
            {
                exceptionHandlers[type].Invoke(context);
                return;
            }
            HandleUnknownException(context);
        }

        private void HandleCodeException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                var exception = context.Exception;
                if (exception != null)
                {
                    ErrorResponseModel value = new ErrorResponseModel(exception, 400, false);
                    context.Result = new ObjectResult(value)
                    {
                        StatusCode = 400
                    };
                    logger.Error($"{exception.Message} \n{JsonSerializer.Serialize(value, options)}\n\n");
                }

                context.ExceptionHandled = true;
            }
        }

        private void HandleException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                Exception exception = context.Exception;
                if (exception != null)
                {
                    ErrorResponseModel value = new ErrorResponseModel(exception, 400);
                    context.Result = new ObjectResult(value)
                    {
                        StatusCode = 400
                    };
                    logger.Error($"\n\nWystąpił wyjątek w aplikacji. \n{JsonSerializer.Serialize(value, options)}\n\n");
                }

                context.ExceptionHandled = true;
            }
        }

        private void HandleForbiddenException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                var exception = context.Exception;
                if (exception != null)
                {
                    ErrorResponseModel value = new ErrorResponseModel(exception, 403, false);
                    context.Result = new ObjectResult(value)
                    {
                        StatusCode = 403
                    };
                    logger.Error($"{exception.Message} \n{JsonSerializer.Serialize(value, options)}\n\n");
                }

                context.ExceptionHandled = true;
            }
        }

        private void HandleNotFoundException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                var exception = context.Exception as NotFoundException;
                if (exception != null)
                {
                    ErrorResponseModel value = new ErrorResponseModel(exception, 404, false);
                    context.Result = new ObjectResult(value)
                    {
                        StatusCode = 404
                    };
                    logger.Error($"\n\nRekord nie znaleziony. \n{JsonSerializer.Serialize(value, options)}\n\n");
                }

                context.ExceptionHandled = true;
            }
        }

        private void HandleUnknownException(ExceptionContext context)
        {
            HandleException(context);
        }

        private void HandValidationleException(ExceptionContext context)
        {
            if (context.ExceptionHandled == false)
            {
                Exception exception = context.Exception;
                if (exception != null)
                {
                    ErrorResponseModel value = new ErrorResponseModel(exception, 405);
                    context.Result = new ObjectResult(value)
                    {
                        StatusCode = 405
                    };
                    logger.Error($"\n\nWystąpił wyjątek w aplikacji. \n{JsonSerializer.Serialize(value, options)}\n\n");
                }

                context.ExceptionHandled = true;
            }
        }
    }
}