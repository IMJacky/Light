using Light.Common;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace Light.Extension.Middleware
{
    public class ExceptionHandleMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandleMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await ExceptionHandleAsync(context, ex);
            }
        }

        private Task ExceptionHandleAsync(HttpContext context, Exception exception)
        {
            var result = JsonConvert.SerializeObject(new { Error = exception.Message });
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            NLogManager.LogError(exception.Message);
            return context.Response.WriteAsync(result);
        }
    }
}
