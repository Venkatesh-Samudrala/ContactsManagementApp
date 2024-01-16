using Microsoft.AspNetCore.Diagnostics;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ContactsManagement.Services
{
    public class ExceptionMiddleware : IExceptionHandler
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                HandleValidationException(context, ex);
            }
            catch (Exception ex)
            {
                HandleGenericException(context, ex);
            }
        }

        private void HandleValidationException(HttpContext context, ValidationException ex)
        {
            _logger.LogError($"Validation Exception: {ex.Message}");

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/json";

            var response = new { ErrorMessage = ex.Message };
            context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private void HandleGenericException(HttpContext context, Exception ex)
        {
            _logger.LogError($"An unhandled exception occurred: {ex}");

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/json";

            var response = new { ErrorMessage = "An unexpected error occurred." };
            context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        public ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
