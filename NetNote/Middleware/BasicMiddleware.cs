using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using guoguo.Domain.ApplicationService;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace NetNote.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class BasicMiddleware
    {
        private readonly RequestDelegate _next;
        public const string AuthorizationHeader = "Authorization";
        public const string WWWAuthenticateHeader = "WWW-Authenticate";
        private IBasicUserRepository _UserRepository;

        public BasicMiddleware(RequestDelegate next, IBasicUserRepository userRepository)
        {
            _next = next;
            _UserRepository = userRepository;
        }

        public Task Invoke(HttpContext httpContext)
        {
            HttpRequest request = httpContext.Request;
            string auth = request.Headers[AuthorizationHeader];
            if (auth == null)
            {
                return BasicResult(httpContext);
            }

            string[] authParts = auth.Split(" ");
            if (authParts.Length != 2)
            {
                return BasicResult(httpContext);
            }

            string base64 = authParts[1];
            string authValue;
            try
            {
                byte[] bytes = Convert.FromBase64String(base64);
                authValue = Encoding.ASCII.GetString(bytes);
            }
            catch
            {
                authValue = null;
            }
            if (string.IsNullOrEmpty(authValue))
            {
                return BasicResult(httpContext);
            }

            string userName;
            string password;
            int sepIndex = authValue.IndexOf(':');
            if (sepIndex < 0)
            {
                userName = authValue;
                password = "";
            }
            else
            {
                userName = authValue.Substring(0, sepIndex);
                password = authValue.Substring(sepIndex + 1);
            }

            if (_UserRepository.Validate(userName,password))
            {
                return _next(httpContext);
            }
            else
            {
                return BasicResult(httpContext);
            }

        }

        private static Task BasicResult(HttpContext httpContext)
        {
            httpContext.Response.StatusCode = 401;
            httpContext.Response.Headers.Add(WWWAuthenticateHeader, "Basic realm=\"localhost\"");
            return Task.FromResult(httpContext);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class BasicMiddlewareExtensions
    {
        public static IApplicationBuilder UseBasicMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<BasicMiddleware>();
        }
    }
}
