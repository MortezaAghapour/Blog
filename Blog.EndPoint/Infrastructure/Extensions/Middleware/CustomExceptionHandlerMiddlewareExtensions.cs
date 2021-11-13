using Blog.EndPoint.Infrastructure.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace Blog.EndPoint.Infrastructure.Extensions.Middleware
{
    public static class CustomExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseCustomExceptionHandler(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }
    }
}
