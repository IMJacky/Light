using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Light.EFRespository
{
    /// <summary>
    /// 工作单元接口定义
    /// </summary>
    public interface IUnitOfWork<TDbContext> : IDisposable where TDbContext : DbContext
    {
        /// <summary>
        /// 获取指定实体的仓储
        /// </summary>
        /// <typeparam name="T">要获取的实体</typeparam>
        /// <returns>当前仓储</returns>
        IRepository<T> GetRepository<T>() where T : class;

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns>影响的行数</returns>
        int SaveChanges();

        /// <summary>
        /// 保存[异步]
        /// </summary>
        /// <returns>影响的行数</returns>
        Task<int> SaveChangesAsync();
    }
}
