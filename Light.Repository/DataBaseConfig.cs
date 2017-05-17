using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Light.Repository
{
    /// <summary>
    /// 数据库配置
    /// </summary>
    public static class DataBaseConfig
    {
        #region SqlServer链接配置
        /// <summary>
        /// 默认的Sql Server的链接字符串
        /// </summary>
        public static string DefaultSqlConnectionString;
        public static IDbConnection GetSqlConnection(string sqlConnectionString = null)
        {
            if (string.IsNullOrWhiteSpace(sqlConnectionString))
            {
                sqlConnectionString = DefaultSqlConnectionString;
            }
            IDbConnection conn = new SqlConnection(sqlConnectionString);
            conn.Open();
            return conn;
        }

        #endregion
    }
}
