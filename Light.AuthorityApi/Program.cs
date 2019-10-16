using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Light.EFRespository;
using Light.EFRespository.LightAuthority;
using Light.EFRespository.LightLog;
using Microsoft.Extensions.Hosting;

namespace Light.AuthorityApi
{
    /// <summary>
    /// 
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 程序入口
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                #region Init DataBases
                var lightLogContext = services.GetRequiredService<LightLogContext>();
                lightLogContext.Database.EnsureCreated();

                var lightAuthorityContext = services.GetRequiredService<LightAuthorityContext>();
                var unitOfWork = services.GetRequiredService<IUnitOfWork<LightAuthorityContext>>();
                LightAuthorityDataSeed.SeedAsync(lightAuthorityContext, unitOfWork).Wait();
                #endregion
            }
            host.Run();
        }

        /// <summary>
        /// 构造Web宿主
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
