using System.Net;
using System.Text.Json;

namespace IncidentManagementSystem.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(
            RequestDelegate next,
            ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled exception occurred");

                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = context.Response;
            response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var errorCode = "INTERNAL_SERVER_ERROR";
            var message = "An unexpected error occurred";

            if (exception is KeyNotFoundException)
            {
                statusCode = HttpStatusCode.NotFound;
                errorCode = "RESOURCE_NOT_FOUND";
                message = exception.Message;
            }
            else if (exception is ArgumentException)
            {
                statusCode = HttpStatusCode.BadRequest;
                errorCode = "INVALID_REQUEST";
                message = exception.Message;
            }

            response.StatusCode = (int)statusCode;

            var errorResponse = new
            {
                errorCode,
                message,
                traceId = context.TraceIdentifier
            };

            return response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }
    }
}
