using Microsoft.AspNetCore.Http;
using MyChat.Classes;
using MyChat.Classes.Exceptions;
using System;
using System.Threading.Tasks;

namespace MyChat.Middleware
{
    public class ErrorHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ErrorHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            httpContext.Response.OnStarting((Func<Task>)(() =>
            {
                httpContext.Response.Headers.Add("X-BuildNumber", SystemVariables.BuildNumber);

                return Task.CompletedTask;
            }));

            try
            {
                await this._next(httpContext);
            }
            catch(APIException e)
            {
                // Added this in a scoped service to transfer the exception to the error view, scoped would not work because of the redirect.
                // Also tried adding it to the session, but when redirecting this is also lost.
                // Will have to find a better way to return API error view than this.
                await httpContext.Response.WriteAsync(
                    "<div style=\"display: flex; flex-direction: column; width: 100%; height: 100%; align-items: center; justify-content: center;\">" +
                        "<img style=\"max-width: 300px;\" src=\"/images/logo_transparent1.png\"/>" +
                        "<h1>API Error</h1>" +
                        "<p>Seems like something is wrong with out API. Please contact systemadministrator.</p>" +
                    "</div>");
            }
        }
    }
}