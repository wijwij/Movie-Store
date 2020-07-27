using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace MovieStore.MVC.Helpers
{
    public class MovieStoreExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        // ASP.NET Core logging is builtin to the framework
        private readonly ILogger<MovieStoreExceptionMiddleware> _logger;

        public MovieStoreExceptionMiddleware(RequestDelegate next, ILogger<MovieStoreExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                // Call the next delegate/middleware in the pipeline
                await _next(httpContext);
            }
            catch (Exception e)
            {
                await HandleException(httpContext, e);
            }
        }

        public async Task HandleException(HttpContext httpContext, Exception exception)
        {
            /*
             * 1 Log the exception details: exception message, exception stack trace, when the exception happened(Datetime), user info, code position.
             *   Most popular logging frameworks in .Net are Serilog(pick), NLog and Log4net (Nuget)
             * 2 Send notification(email preferred) to the Dev team.
             *   MailKit(free) -- send emails, SendGrid(paid, free trial
             * 3 Display a friendly error page to the User
             */
            _logger.LogInformation("--------START OF LOGGING----------");
            _logger.LogError($"Exception Message: {exception.Message}");
            _logger.LogError($"Exception Stack Trace: {exception.StackTrace}");
            _logger.LogInformation($"Exception for User: {httpContext.User.Identity.Name}");
            _logger.LogInformation($"Exception happened on {DateTime.UtcNow}");
            _logger.LogInformation("--------END OF LOGGING----------");

            httpContext.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
            httpContext.Response.Redirect("/Home/Error");
            await Task.CompletedTask;
        }
    }

    public static class MovieStoreExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseMovieStoreExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<MovieStoreExceptionMiddleware>();
        }
    }
}