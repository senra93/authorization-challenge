using Authorization.Business.Interfaces;
using Microsoft.AspNetCore.Authentication;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace Authorization.Middlewares
{
    public class AuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IUserService userService)
        {
            try
            {
                if (!context.Request.Headers.ContainsKey("Authorization"))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                var header = context.Request.Headers["Authorization"].ToString();

                if (!header.StartsWith("Basic ", StringComparison.OrdinalIgnoreCase))
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                var encodedCreds = header.Substring(6);
                var creds = Encoding.UTF8.GetString(Convert.FromBase64String(encodedCreds));
                string[] userPassword = creds.Split(':');
                var user = userPassword[0];
                var password = userPassword[1];

                var userDto = await userService.GetUserByCredentials(user, password);

                if (userDto == null)
                {
                    context.Response.StatusCode = 401;
                    await context.Response.WriteAsync("Unauthorized");
                    return;
                }

                await _next(context);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            _next(context);
        }
    }
}
