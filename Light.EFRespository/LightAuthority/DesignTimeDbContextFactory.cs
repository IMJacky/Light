using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Light.EFRespository.LightAuthority
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<LightAuthorityContext>
    {
        public LightAuthorityContext CreateDbContext(string[] args)
        {
            Directory.SetCurrentDirectory("..");//设置当前路径为当前解决方案的路径
            string appSettingBasePath = Directory.GetCurrentDirectory() + "/Light.AuthorityApi";//改成你的appsettings.json所在的项目名称
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(appSettingBasePath)
                .AddJsonFile("appsettings.json")
                .Build();

            var builder = new DbContextOptionsBuilder<LightAuthorityContext>();
            builder.UseSqlServer(configBuilder.GetConnectionString("LightConnection"));
            return new LightAuthorityContext(builder.Options);
        }
    }
}
