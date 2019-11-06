using Light.Common;
using Microsoft.AspNetCore.Http;
using System;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Light.Extension.Middleware
{
    public class RequestResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            HttpRequest request = context.Request;
            request.EnableBuffering();//多次读取
            StringBuilder stringBuilder = new StringBuilder();
            string requestBody = string.Empty;
            if (request.ContentLength.HasValue)
            {
                requestBody = await new StreamReader(request.Body).ReadToEndAsync();//如果使用using的话将leaveOpen参数置为true，确保可以读取之后不关闭
                request.Body.Seek(0, SeekOrigin.Begin);
                //var buffer = new byte[request.ContentLength.Value];
                //await request.Body.ReadAsync(buffer, 0, buffer.Length);//使用这种方法会造成A non-empty request body is required错误
                //requestBody = Encoding.UTF8.GetString(buffer);
            }
            stringBuilder.AppendFormat("请求路径：{0}://{1}{2}{3}，请求方式：{4}，请求体：{5}",
                request.Scheme, request.Host, request.Path, request.QueryString, request.Method, requestBody);

            var originalBodyStream = context.Response.Body;
            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;
                await _next(context);
                stopwatch.Stop();
                var response = context.Response;
                response.Body.Seek(0, SeekOrigin.Begin);
                string responseText = await new StreamReader(response.Body).ReadToEndAsync();
                response.Body.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);

                stringBuilder.AppendFormat("，耗时：{0}ms，响应码：{1}，响应体：{2}", stopwatch.ElapsedMilliseconds, response.StatusCode, responseText);
            }

#if !DEBUG
            NLogManager.LogTrace(stringBuilder.ToString()); 
#endif
        }
    }
}
