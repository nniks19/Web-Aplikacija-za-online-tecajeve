using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace WAZOT.Middlewares
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class AuthMiddleware
    {
        private readonly RequestDelegate _next;

        public AuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path; 
            if (path.Value.StartsWith("/Administrator"))
            {
                if(httpContext.Session.GetString("email") == null)
                {
                    httpContext.Response.Redirect("/");
                }
                else
                {
                    if (httpContext.Session.GetString("razina_prava") != "1")
                    {
                        httpContext.Response.Redirect("/");
                    }
                }
            }
            if (path.Value.StartsWith("/Korisnik"))
            {
                if (httpContext.Session.GetString("email") == null)
                {
                    httpContext.Response.Redirect("/");

                }
                else
                {
                    if (httpContext.Session.GetString("razina_prava") != "2")
                    {
                        httpContext.Response.Redirect("/");
                    }
                }
            }
            if (path.Value.StartsWith("/Kreator_Tecaja"))
            {
                if (httpContext.Session.GetString("email") == null)
                {
                    httpContext.Response.Redirect("/");

                }
                else
                {
                    if (httpContext.Session.GetString("razina_prava") != "3")
                    {
                        httpContext.Response.Redirect("/");
                    }
                }
            }
            return _next(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class AuthentificationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthentificationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthMiddleware>();
        }
    }
}
