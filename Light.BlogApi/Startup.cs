using System;
using System.IO;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Light.EFRespository;
using Light.EFRespository.LightBlog;
using Light.EFRespository.LightLog;
using Light.Extension.Filter;
using Light.Extension.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;
using Light.Common;
using Microsoft.OpenApi.Models;

namespace Light.BlogApi
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 启动构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 配置信息
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        ///  This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<LightBlogContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("LightBlogConnectionMySql")));
            services.AddDbContext<LightLogContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("LightLogConnectionMySql")));

            services.AddScoped<IUnitOfWork<LightBlogContext>, UnitOfWork<LightBlogContext>>();
            services.AddScoped<IUnitOfWork<LightLogContext>, UnitOfWork<LightLogContext>>();

            services.RegisterAllService();
            services.AddSwaggerGen(m =>
            {
                m.SwaggerDoc("v1", new OpenApiInfo
                {
                    Description = "Light.BlogApi",
                    Contact = new OpenApiContact { Email = "871834898@qq.com", Name = "王杰光", Url = new Uri("http://jiqunar.com") },
                    Version = "v1",
                    Title = "BlogAPI"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                var xmlPathModel = Path.Combine(AppContext.BaseDirectory, "Light.Model.xml");
                m.IncludeXmlComments(xmlPath);
                m.IncludeXmlComments(xmlPathModel);
            });

            services.AddRouting(m =>
            {
                m.LowercaseUrls = true;
            });
            services.AddCors(m =>
            {
                m.AddPolicy("Default", n =>
                {
                    n.AllowAnyHeader();
                    n.AllowAnyMethod();
                    n.AllowAnyOrigin();
                });
            });
            //services.AddEventBus();
            services.AddControllers(m =>
            {
                //使用ExceptionHandleFilter或者ExceptionHandleMiddleware
                //m.Filters.Add<ExceptionHandleFilter>();
                m.Filters.Add<ValidatoHandleFilter>();
            })
            //设置WebAPI的Json返回的格式
            .AddNewtonsoftJson(m =>
            {
                //不使用驼峰命名，否则首字母会小写
                m.SerializerSettings.ContractResolver = new DefaultContractResolver();
                m.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
                m.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                m.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            })
            .AddFluentValidation(m => m.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "http://10.102.40.72:4999";
                options.RequireHttpsMetadata = false;
                options.ApiName = "BlogApi";
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //使用ExceptionHandleFilter或者ExceptionHandleMiddleware
            app.UseMiddleware<ExceptionHandleMiddleware>();
            app.UseSwagger();
            app.UseSwaggerUI(m =>
            {
                m.SwaggerEndpoint("/swagger/v1/swagger.json", "Light.Blog API V1");
                m.RoutePrefix = "lightblog";
            });
            var environmentName = env.EnvironmentName;
            //app.UseMiddleware<RequestElapseMiddleware>();
            app.UseRouting();
            app.UseCors("Default");
            app.UseAuthentication();
            app.UseAuthorization();//这两个位置不能颠倒
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
