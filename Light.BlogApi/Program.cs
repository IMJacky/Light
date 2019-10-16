using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Light.EFRespository;
using Light.EFRespository.LightBlog;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Light.BlogApi
{
    /// <summary>
    /// 入口
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 入口方法
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                #region Init DataBases
                var lightAuthorityContext = services.GetRequiredService<LightBlogContext>();
                var unitOfWork = services.GetRequiredService<IUnitOfWork<LightBlogContext>>();
                LightBlogDataSeed.SeedAsync(lightAuthorityContext, unitOfWork).Wait();
                #endregion
            }
            host.Run();
        }
        /// <summary>
        /// 创建HostBuilder
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webBuilder =>
            {
                webBuilder.ConfigureKestrel(serverOptions =>
                {
                    // Set properties and call methods on options
                })
                .UseStartup<Startup>();
            });
    }
}
