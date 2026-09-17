using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HotelBooking.API.Middlewares
{
    public class GlobalExeptionMiddleware(IProblemDetailsService problemDetailsService,
    ILogger<GlobalExeptionMiddleware> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext,
            Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError(exception, "UnHandler Exception");
            httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
            var ProblemDetailsContext = new ProblemDetailsContext
            {
                HttpContext = httpContext,
                Exception = exception,
                ProblemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status500InternalServerError,
                    Title = "Internal Server Error",
                    Detail = exception.Message,
                    Instance = httpContext.Request.Path
                }
            };

            return await problemDetailsService.TryWriteAsync(ProblemDetailsContext);
        }
    }
}
