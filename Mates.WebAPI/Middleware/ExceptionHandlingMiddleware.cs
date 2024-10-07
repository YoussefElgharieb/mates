using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Mates.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                var statusCode = exception switch
                {
                    ArgumentException => (int)HttpStatusCode.BadRequest,
                    UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
                    DbUpdateException => (int)HttpStatusCode.Conflict,
                    _ => (int)HttpStatusCode.InternalServerError
                };

                httpContext.Response.StatusCode = statusCode;

                var response = new { error = exception.Message };

                await httpContext.Response.WriteAsJsonAsync(response);
            }
        }
    }
}
