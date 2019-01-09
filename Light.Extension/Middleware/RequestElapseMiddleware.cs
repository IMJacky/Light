using Light.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Light.Extension.Middleware
{
    public class RequestElapseMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestElapseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            HttpRequest httpRequest = context.Request;
            HttpResponse httpResponse = context.Response;

            if (httpResponse.HasStarted)
            {
                return;
            }

            StringBuilder stringBuilder = new StringBuilder(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
            stringBuilder.AppendFormat("，请求路径：{0}{1}{2}，请求方式：{3}",
                httpRequest.Host.HasValue ? httpRequest.Host.Value : string.Empty,
                httpRequest.Path.HasValue ? httpRequest.Path.Value : string.Empty,
                httpRequest.QueryString.HasValue ? httpRequest.QueryString.Value : string.Empty,
                httpRequest.Method);

            httpRequest.EnableBuffering();
            httpRequest.Body.Seek(0, SeekOrigin.Begin);
            string requestText = await new StreamReader(httpRequest.Body).ReadToEndAsync();
            httpRequest.Body.Seek(0, SeekOrigin.Begin);
            stringBuilder.AppendFormat("，RequestBody：{0}", requestText);

            //await _next(context);
            //stopwatch.Stop();
            //stringBuilder.AppendFormat("，耗时：{0}ms", stopwatch.ElapsedMilliseconds);

            //可能有性能消耗
            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                httpResponse.Body = responseBody;
                await _next(context);
                stopwatch.Stop();
                httpResponse.Body.Seek(0, SeekOrigin.Begin);
                string responseText = await new StreamReader(httpResponse.Body).ReadToEndAsync();
                httpResponse.Body.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
                stringBuilder.AppendFormat("，ResponseBody：{0}，耗时：{1}ms", responseText, stopwatch.ElapsedMilliseconds);
            }

            NLogManager.LogTrace(stringBuilder.ToString());
        }
    }
}
