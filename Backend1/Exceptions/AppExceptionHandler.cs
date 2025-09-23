using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http.HttpResults;

namespace Backend1.Exceptions
{
    public class AppExceptionHandler : IExceptionHandler
    {
        public readonly ILogger<AppExceptionHandler> _logger;
        public AppExceptionHandler(ILogger<AppExceptionHandler> logger) 
        { 
                _logger = logger;        
        
        }
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int statusCode, string errorMessage) = exception switch
            {
                UnauthorizedAccessException => (403, null),
                BadHttpRequestException badRequestException => (400, badRequestException.Message),
                KeyNotFoundException  notFoundException => (404, notFoundException.Message),
                _ => (500, "Something went wrong")
            };

            if (statusCode == 500)
            {
                return false;
            }

            _logger.LogError(exception, exception.Message);
            httpContext.Response.StatusCode = statusCode;
            httpContext.Response.ContentType = "application/json";

            var response = new
            {
                StatusCode = statusCode,
                Message = errorMessage
            };
            await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);

            return true;
        }
    }
}
