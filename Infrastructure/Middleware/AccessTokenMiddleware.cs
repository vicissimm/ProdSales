
using Microsoft.AspNetCore.Http;
using Infrastructure.Repository;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Middleware
{
    public class AccessTokenMiddleware
    {
        private readonly RequestDelegate _next;

        public AccessTokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, Authorization authorization)
        {
            string accessToken = context.Request.Headers["accessToken"].FirstOrDefault()?.Split(" ").Last();

            if (!string.IsNullOrEmpty(accessToken))
            {
                try
                {
                    var userObject = authorization.DecodeAccessToken(accessToken);
                    System.Diagnostics.Debug.WriteLine(userObject);
                    // Set the userId in HttpContext so that it can be accessed in subsequent middleware or controllers
                    context.Items["UserId"] = userObject.Id;
                    context.Items["IsAdmin"] = userObject.IsAdmin;   
                }
                catch (SecurityTokenValidationException)
                {
                    // Token validation failed, return unauthorized response
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
                catch (Exception ex)
                {
                    // Other exceptions occurred, log the error and return unauthorized response
                    Console.WriteLine($"Error decoding access token: {ex.Message}");
                    context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                    return;
                }
            }

            await _next(context);
        }

    }
}