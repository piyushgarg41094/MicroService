using Newtonsoft.Json;
using Serilog;
using System.Net;

namespace Mango.Web.Middlewares
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                Log.Error(ex, "An unhandled exception occurred while processing the request.");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var code = HttpStatusCode.InternalServerError; // Default to 500
            var errorMessage = "An unexpected error occurred. Please try again later.";

            // Log different exceptions at different levels
            switch (exception)
            {
                case UnauthorizedAccessException:
                    code = HttpStatusCode.Unauthorized;
                    Log.Warning(exception, "Unauthorized access attempt.");
                    break;
                case ArgumentException:
                    code = HttpStatusCode.BadRequest;
                    Log.Warning(exception, "Bad request error.");
                    break;
                case KeyNotFoundException:
                    code = HttpStatusCode.NotFound;
                    Log.Warning(exception, "Requested resource was not found.");
                    break;
                default:
                    Log.Error(exception, "An unexpected error occurred.");
                    break;
            }
            var result = JsonConvert.SerializeObject(new { error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)code;
            return context.Response.WriteAsJsonAsync(result);
        }
    }
}
