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
using Microsoft.Extensions.Logging;

namespace Light.BlogApi
{
    public class Program
    {
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

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
