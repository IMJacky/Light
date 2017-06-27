using System;
using System.Collections.Generic;
using Light.IRepository;
using System.Data;
using Dapper;
using System.Threading.Tasks;
using Light.Model.TableModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace Light.Repository
{

    /// <summary>
    /// 用户仓储
    /// </summary>
    public class UserRepository : IUserRepository
    {
        /// <summary>
        /// 创建一个用户
        /// </summary>
        /// <param name="entity">用户</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public bool CreateEntity(User entity, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string insertSql = @"INSERT INTO [dbo].[User]
                                           ([UserName]
                                           ,[Password]
                                           ,[Gender]
                                           ,[Birthday]
                                           ,[CreateUserId]
                                           ,[CreateDate]
                                           ,[UpdateUserId]
                                           ,[UpdateDate]
                                           ,[IsDeleted])
                                     VALUES
                                           (@UserName
                                           ,@Password
                                           ,@Gender
                                           ,@Birthday
                                           ,@CreateUserId
                                           ,@CreateDate
                                           ,@UpdateUserId
                                           ,@UpdateDate
                                           ,@IsDeleted)";
                return conn.Execute(insertSql, entity) > 0;
            }
        }

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entityList">要创建的实体</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public bool CreateEntityList(IEnumerable<User> entityList, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string insertSql = @"INSERT INTO [dbo].[User]
                                           ([UserName]
                                           ,[Password]
                                           ,[Gender]
                                           ,[Birthday]
                                           ,[CreateUserId]
                                           ,[CreateDate]
                                           ,[UpdateUserId]
                                           ,[UpdateDate]
                                           ,[IsDeleted])
                                     VALUES
                                           (@UserName
                                           ,@Password
                                           ,@Gender
                                           ,@Birthday
                                           ,@CreateUserId
                                           ,@CreateDate
                                           ,@UpdateUserId
                                           ,@UpdateDate
                                           ,@IsDeleted)";
                return conn.Execute(insertSql, entityList) > 0;
            }
        }

        /// <summary>
        /// 根据主键Id删除一个用户
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public bool DeleteEntityById(int id, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string deleteSql = @"DELETE FROM [dbo].[User]
                                            WHERE Id = @Id";
                return conn.Execute(deleteSql, new { Id = id }) > 0;
            }
        }

        /// <summary>
        /// 获取所有用户
        /// </summary>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public IEnumerable<User> RetriveAllEntity(string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id]
                                          ,[UserName]
                                          ,[Password]
                                          ,[Gender]
                                          ,[Birthday]
                                          ,[CreateUserId]
                                          ,[CreateDate]
                                          ,[UpdateUserId]
                                          ,[UpdateDate]
                                          ,[IsDeleted]
                                      FROM [dbo].[User]";
                return conn.Query<User>(querySql);
            }
        }

        /// <summary>
        /// 根据主键Id获取一个用户
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public User RetriveOneEntityById(int id, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string querySql = @"SELECT [Id]
                                          ,[UserName]
                                          ,[Password]
                                          ,[Gender]
                                          ,[Birthday]
                                          ,[CreateUserId]
                                          ,[CreateDate]
                                          ,[UpdateUserId]
                                          ,[UpdateDate]
                                          ,[IsDeleted]
                                      FROM [dbo].[User]
                                     WHERE Id = @Id";
                return conn.QueryFirstOrDefault<User>(querySql, new { Id = id });
            }
        }

        /// <summary>
        /// 修改一个用户
        /// </summary>
        /// <param name="entity">要修改的用户</param>
        /// <param name="connectionString">链接字符串</param>
        /// <returns></returns>
        public bool UpdateEntity(User entity, string connectionString = null)
        {
            using (IDbConnection conn = DataBaseConfig.GetSqlConnection(connectionString))
            {
                string updateSql = @"UPDATE [dbo].[User]
                                       SET [UserName] = @UserName
                                          ,[Password] = @Password
                                          ,[Gender] = @Gender
                                          ,[Birthday] = @Birthday
                                          ,[UpdateUserId] = @UpdateUserId
                                          ,[UpdateDate] = @UpdateDate
                                          ,[IsDeleted] = @IsDeleted
                                     WHERE Id = @Id";
                return conn.Execute(updateSql, entity) > 0;
            }
        }
    }
}
