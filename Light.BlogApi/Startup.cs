using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Light.EFRespository;
using Light.EFRespository.LightBlog;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Serialization;

namespace Light.BlogApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LightBlogContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("LightBlogConnectionMySql")));
            services.AddScoped<IUnitOfWork<LightBlogContext>, UnitOfWork<LightBlogContext>>();
            services.AddMvc()
            //设置WebAPI的Json返回的格式
            .AddJsonOptions(m =>
            {
                //不使用驼峰命名，否则首字母会小写
                m.SerializerSettings.ContractResolver = new DefaultContractResolver();
                m.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
