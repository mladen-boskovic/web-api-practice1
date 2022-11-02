using API.Core.Errors;
using Application.Exceptions;
using Application.Loggers;
using FluentValidation;

namespace API.Core
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IExceptionLogger logger)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                logger.Log(ex);

                httpContext.Response.ContentType = "application/json";
                object response = null;
                var statusCode = StatusCodes.Status500InternalServerError;

                if (ex is GlobalException e)
                {
                    statusCode = e.StatusCode;

                    if (!string.IsNullOrWhiteSpace(e.Message))
                    {
                        response = new GlobalExceptionError { ErrorMessage = e.Message };
                    }
                }

                if (ex is ValidationException ve)
                {
                    statusCode = StatusCodes.Status422UnprocessableEntity;
                    response = ve.Errors.Select(x => new ValidationError
                    {
                        Property = x.PropertyName,
                        ErrorMessage = x.ErrorMessage
                    });
                }

                httpContext.Response.StatusCode = statusCode;

                if (response != null)
                {
                    await httpContext.Response.WriteAsJsonAsync(response);
                }
            }
        }
    }
}
