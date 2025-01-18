using Serilog.Context;
using System.Net;
using System.Security.Claims;
using System.Text.Json;

namespace EPYSLSAILORAPP.SystemServices
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public GlobalExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                var claim = httpContext.User.Identity as ClaimsIdentity;

                List<Claim> listClaims = (List<Claim>)claim.Claims;

                if (listClaims.Count == 0)
                {
                    //throw new Exception("Unauthorizeed User");
                }

                await _next(httpContext);
            }
            catch (Exception ex)
            {
               
                await HandleExceptionAsync(httpContext, ex);

                
            }
        }
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var objError = new ErrorDetails()
            {
                StatusCode = context.Response.StatusCode,
                Message = "Internal Server Error from the custom middleware.",
                ActualError = exception.ToString()
            };

            using (LogContext.PushProperty("SpecialLogType", true))
            {
                _logger.LogError(exception.ToString());
            }
            
      

            await context.Response.WriteAsync(objError.ToString());


        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class GlobalExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<GlobalExceptionMiddleware>();
        }
    }

    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public string ActualError { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
