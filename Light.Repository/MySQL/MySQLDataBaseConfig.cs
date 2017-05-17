using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
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
        public static string DefaultMySqlConnectionString;
        public static LightContext CreateContext(string mySqlConnectionString = null)
        {
            if (string.IsNullOrWhiteSpace(mySqlConnectionString))
            {
                mySqlConnectionString = DefaultMySqlConnectionString;
            }
            var optionBuilder = new DbContextOptionsBuilder<LightContext>();
            optionBuilder.UseMySQL(mySqlConnectionString);
            var context = new LightContext(optionBuilder.Options);
            context.Database.EnsureCreated();
            return context;
        }
    }
}
