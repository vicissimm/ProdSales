using Infrastructure.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Infrastructure.Extensions
{
    public static class AccessTokenMiddlewareExtensions
    {
        public static IApplicationBuilder UseAccessTokenMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AccessTokenMiddleware>();
        }
    }
}
