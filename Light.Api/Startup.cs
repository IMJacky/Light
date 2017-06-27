using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Light.DependencyInjection;

namespace Light.Api
{
    /// <summary>
    /// 项目启动类
    /// </summary>
    public class Startup
    {
        ///// <summary>
        ///// 构造函数
        ///// </summary>
        ///// <param name="env"></param>
        //public Startup(IHostingEnvironment env)
        //{
        //    var builder = new ConfigurationBuilder()
        //        .SetBasePath(env.ContentRootPath)
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
        //        .AddEnvironmentVariables();
        //    Configuration = builder.Build();
        //}

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置类
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            //string defaultSqlConnectionString = Configuration.GetConnectionString("SqlServerConnection");
            string defaultMySqlConnectionString = Configuration.GetConnectionString("MySqlConnection");

            RepositoryInjection.ConfigureRepository(services);
            BusinessInjection.ConfigureBusiness(services);
            services.AddMvc();
            services.AddSwaggerGen(m =>
            {
                m.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info { Title = "LightApi", Version = "v1", Description = "Light接口文档" });
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseMvc();
            app.UseSwagger(m =>
            {
                m.PreSerializeFilters.Add((swagger, http) =>
                {
                    swagger.Host = http.Host.Value;
                });
            });
            app.UseSwaggerUI(m =>
            {
                m.RoutePrefix = "wjg";
                m.SwaggerEndpoint("/swagger/v1/swagger.json", "Light接口文档");
            });
        }
    }
}
