using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
namespace Light.Repository.MySQL
{
    /// <summary>
    /// MySQL的数据库配置
    /// </summary>
    public class MySQLDataBaseConfig
    {
        /// <summary>
        /// 默认的Sql Server的链接字符串
        /// </summary>
        private const string DefaultMySqlConnectionString = "server=localhost;userid=root;pwd=sasa;port=3306;database=light;";
        public static LightContext CreateContext(string mySqlConnectionString = null)
        {
            if (string.IsNullOrWhiteSpace(mySqlConnectionString))
            {
                mySqlConnectionString = DefaultMySqlConnectionString;
            }
            var optionBuilder = new DbContextOptionsBuilder<LightContext>();
            //var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;
            optionBuilder.UseMySQL(mySqlConnectionString, m =>
            {
                
            });
            var context = new LightContext(optionBuilder.Options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
