using Light.Service.Event;
using Light.Service.Event.CustomerEventArgs;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace Light.Extension
{
    public static class ConfigureExtend
    {
        public static void AddEventBus(this IServiceCollection services)
        {
            //services.AddSingleton<IEventBus, EventBus>();
            //var eventBus = new EventBus();
            EventBus.InitEventBus();
        }

        /// <summary>
        /// 注册所有服务
        /// </summary>
        /// <param name="services"></param>
        public static void AddAllServices(this IServiceCollection services)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Light.Service.dll");
            Assembly assembly = Assembly.LoadFrom(path);
            Type[] types = assembly.GetTypes();

            string pathIService = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Light.IService.dll");
            Assembly assemblyIService = Assembly.LoadFrom(pathIService);
            Type[] typesIService = assemblyIService.GetTypes();
            foreach (var type in types)
            {
                if (type.Name.Contains("Service"))
                {
                    var iService = "I" + type.Name;
                    foreach (var typeIService in typesIService)
                    {
                        if (typeIService.Name.Equals(iService))
                        {
                            services.AddScoped(typeIService, type);
                        }
                    }
                }
            }
        }
    }
}
