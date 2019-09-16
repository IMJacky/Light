using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.AspNetCore;
using Light.EFRespository;
using Light.EFRespository.LightBlog;
using Light.EFRespository.LightLog;
using Light.Extension;
using Light.Extension.Filter;
using Light.Extension.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Swashbuckle.AspNetCore.Swagger;

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
            services.AddDbContext<LightLogContext>(options =>
                options.UseMySql(Configuration.GetConnectionString("LightLogConnectionMySql")));

            services.AddScoped<IUnitOfWork<LightBlogContext>, UnitOfWork<LightBlogContext>>();
            services.AddScoped<IUnitOfWork<LightLogContext>, UnitOfWork<LightLogContext>>();
            
            services.AddAllServices();
            services.AddSwaggerGen(m =>
            {
                m.SwaggerDoc("v1", new Info
                {
                    Description = "Light.BlogApi",
                    Contact = new Contact { Email = "871834898@qq.com", Name = "王杰光", Url = "http://jiqunar.com" },
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
                m.SerializerSettings.DateTimeZoneHandling = DateTimeZoneHandling.Local;
                m.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
            })
            .AddFluentValidation(m => m.RegisterValidatorsFromAssemblyContaining<Startup>())
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAuthentication("Bearer")
            .AddIdentityServerAuthentication(options =>
            {
                options.Authority = "http://localhost:4999";
                options.RequireHttpsMetadata = false;
                options.ApiName = "BlogApi";
            });

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
