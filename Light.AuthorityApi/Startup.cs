using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;
using Newtonsoft.Json.Serialization;
using System.Reflection;
using Microsoft.AspNetCore.Http;
using Light.Service.Authority;
using Light.IService.LightAuthority;
using System;
using FluentValidation.AspNetCore;
using Light.EFRespository;
using Light.Extension.Middleware;
using Light.Extension.Filter;
using Light.Extension;
using Light.EFRespository.LightAuthority;
using Light.EFRespository.LightLog;
using Light.Common;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Light.Model;
using Newtonsoft.Json;
using Microsoft.OpenApi.Models;

namespace Light.AuthorityApi
{
    /// <summary>
    /// 启动类
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        /// <summary>
        /// Configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            #region 暂时注释，因为改成UnitOfWork了
            //services.AddTransient<LightContext>(factory =>
            //{
            //    var builder = new DbContextOptionsBuilder<LightContext>();
            //    //builder.UseSqlServer(Configuration.GetConnectionString("LightConnection"));
            //    builder.UseMySql(Configuration.GetConnectionString("LightAuthorityConnectionMySql"));

            //    var accessor = factory.GetService<IHttpContextAccessor>();
            //    bool? isDeleted = false;//默认全局查询未删除的数据
            //    if (accessor != null && accessor.HttpContext != null)
            //    {
            //        string method = accessor.HttpContext.Request.Method.ToLower();
            //        StringValues queryIsdeleted = StringValues.Empty;
            //        if (method == "get")
            //        {
            //            queryIsdeleted = accessor.HttpContext.Request.Query["d"];//不用IsDeleted，用一个高大上的d
            //        }
            //        else if (method == "post" && accessor.HttpContext.Request.HasFormContentType)
            //        {
            //            queryIsdeleted = accessor.HttpContext.Request.Form["d"];
            //        }
            //        if (!StringValues.IsNullOrEmpty(queryIsdeleted))
            //        {
            //            //0：未删除，1：已删除，2：全部
            //            if (int.TryParse(queryIsdeleted.FirstOrDefault(), out int isDeletedInt))
            //            {
            //                if (isDeletedInt == 0)
            //                {
            //                    isDeleted = false;
            //                }
            //                else if (isDeletedInt == 1)
            //                {
            //                    isDeleted = true;
            //                }
            //                else if (isDeletedInt == 2)
            //                {
            //                    isDeleted = null;
            //                }
            //            }
            //        }
            //    }
            //    return new LightContext(builder.Options, isDeleted);
            //});
            #endregion

            // Add DbContext
            services.AddDbContext<LightLogContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("LightLogConnectionMySql")));
            services.AddDbContext<LightAuthorityContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("LightAuthorityConnectionMySql")));
            //services.AddDbContext<LightLogContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("LightLogConnection")));
            //services.AddDbContext<LightAuthorityContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("LightAuthorityConnection")));
            services.AddScoped<IUnitOfWork<LightLogContext>, UnitOfWork<LightLogContext>>();
            services.AddScoped<IUnitOfWork<LightAuthorityContext>, UnitOfWork<LightAuthorityContext>>();
            //services.AddScoped(typeof(IAuthorityService), typeof(AuthorityService));
            services.RegisterAllService("Light.Service");
            services.AddSwaggerGen(m =>
            {
                m.SwaggerDoc("v1", new OpenApiInfo
                {
                    Description = "Light.AuthorityApi",
                    Contact = new OpenApiContact { Email = "871834898@qq.com", Name = "王杰光", Url = new Uri("http://jiqunar.com") },
                    Version = "v1",
                    Title = "LightAPI"
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
            .AddIdentityServerAuthentication("Bearer", options =>
             {
                 options.Authority = "http://localhost:4999";
                 options.RequireHttpsMetadata = false;
                 options.ApiName = "AuthApi";
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
                m.SwaggerEndpoint("/swagger/v1/swagger.json", "Light.Authority API V1");
                m.RoutePrefix = "lightauth";
            });
            var environmentName = env.EnvironmentName;
            //app.UseMiddleware<RequestElapseMiddleware>();
            app.UseCors("Default");
            app.UseAuthentication();
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
