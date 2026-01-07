
namespace CustomMiddleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class FullNameMiddleware
    {
        private readonly RequestDelegate _next;

        public FullNameMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            if (httpContext.Request.Query.ContainsKey("firstname") && httpContext.Request.Query.ContainsKey("lastname"))
            {
                var fullName = httpContext.Request.Query["firstname"] + " " + httpContext.Request.Query["lastname"];
                await httpContext.Response.WriteAsync("\nHello, " + fullName);
            }

            await _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class FullNameMiddlewareExtensions
    {
        public static IApplicationBuilder UseFullNameMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<FullNameMiddleware>();
        }
    }
}
