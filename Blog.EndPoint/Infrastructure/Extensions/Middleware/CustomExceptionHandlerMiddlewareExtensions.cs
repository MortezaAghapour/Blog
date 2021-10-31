using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
