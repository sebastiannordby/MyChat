using Microsoft.AspNetCore.Http;
using MyChat.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyChat.Middleware
{
    public class CustomAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomAuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var requestedPath = context.Request.Path.Value;
            var isAllowedPath = SystemVariables.AllowedPaths.Where(x =>
                x.Equals(requestedPath, StringComparison.InvariantCultureIgnoreCase)).Any();

            var jwToken = context.Session.GetString("JWToken");

            if (isAllowedPath || context.User.Identity.IsAuthenticated)
            {
                await _next(context);
            }
            else
            {
                context.Response.Redirect(context.Request.GetHostURI() + "/Account/Signin");
            }
        }
    }
}
