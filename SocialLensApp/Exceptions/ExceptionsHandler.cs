using Microsoft.AspNetCore.Diagnostics;

namespace SocialLensApp.Exceptions
{
    public class ExceptionsHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            (int statusCode, string errorMessage) = exception switch
            {
                BadRequestException badRequestException => (400, badRequestException.Message),

                _ => (500, "Something went wrong"),
            };
            httpContext.Response.StatusCode = statusCode;
            await httpContext.Response.WriteAsync(errorMessage);
            return true;
        }
    }
}
