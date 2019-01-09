using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Light.EFRespository;
using Light.EFRespository.LightAuthority;
using Light.EFRespository.LightLog;

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
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
