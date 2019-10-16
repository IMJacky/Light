using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Light.Common
{
    /// <summary>
    /// 自动注册服务Attribute
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class RegisterServiceAttribute : Attribute
    {
        /// <summary>
        /// 要实现的Service的接口
        /// </summary>
        public Type IServiceType { get; set; }

        /// <summary>
        /// 生命周期
        /// </summary>
        public ServiceLifetime ServiceLifetime { get; set; } = ServiceLifetime.Scoped;
    }

    /// <summary>
    /// ServiceCollection扩展方法
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static void RegisterAllService(this IServiceCollection services, params string[] assemblyNames)
        {
            foreach (var assemblyName in assemblyNames)
            {
                Assembly assemblie = Assembly.LoadFrom(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, assemblyName + ".dll"));
                var typesInfos = assemblie.DefinedTypes.Where(x => x.GetCustomAttributes().Any(a => a is RegisterServiceAttribute)).ToList();
                foreach (var type in typesInfos)
                {
                    var registerServiceAttribute = type.GetCustomAttribute<RegisterServiceAttribute>();
                    services.Add(new ServiceDescriptor(registerServiceAttribute.IServiceType, type, registerServiceAttribute.ServiceLifetime));
                }
            }
        }
    }
}
