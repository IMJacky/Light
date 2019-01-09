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
            services.AddScoped(typeof(IAuthorityService), typeof(AuthorityService));
            services.AddSwaggerGen(m =>
            {
                m.SwaggerDoc("v1", new Info
                {
                    Description = "Light.AuthorityApi",
                    Contact = new Contact { Email = "871834898@qq.com", Name = "王杰光", Url = "http://jiqunar.com" },
                    Version = "v1",
                    Title = "LightAPI"
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                var xmlPathModel = Path.Combine(AppContext.BaseDirectory, "Light.Model.xml");
                m.IncludeXmlComments(xmlPath);
                m.IncludeXmlComments(xmlPathModel);
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
            services.AddEventBus();
            services.AddMvc(m =>
            {
                //使用ExceptionHandleFilter或者ExceptionHandleMiddleware
                //m.Filters.Add<ExceptionHandleFilter>();
                m.Filters.Add<ValidatoHandleFilter>();
            })
            //设置WebAPI的Json返回的格式
            .AddJsonOptions(m =>
            {
                //不使用驼峰命名，否则首字母会小写
                m.SerializerSettings.ContractResolver = new DefaultContractResolver();
                m.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            })
            .AddFluentValidation(m => m.RegisterValidatorsFromAssemblyContaining<Startup>())
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "http://localhost:5000";
                options.RequireHttpsMetadata = false;

                options.ApiName = "api1";
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //使用ExceptionHandleFilter或者ExceptionHandleMiddleware
            app.UseMiddleware<ExceptionHandleMiddleware>();

            app.UseSwagger();
            app.UseSwaggerUI(m =>
            {
                m.SwaggerEndpoint("/swagger/v1/swagger.json", "Light.Authority API V1");
                m.RoutePrefix = "light";
            });
            var environmentName = env.EnvironmentName;
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();

            }
            else
            {
            }
            //app.UseMiddleware<RequestElapseMiddleware>();
            app.UseCors("Default");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
