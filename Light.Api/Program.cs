using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Light.Api
{
    /// <summary>
    /// 程序文件配置
    /// </summary>
    public class Program
    {
        /// <summary>
        /// 程序入口Main方法
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseUrls("http://localhost:6000")
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
