using Light.Model.CommonModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Light.EFRespository
{
    /// <summary>
    /// 仓储接口定义
    /// </summary>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// 添加一个实体
        /// </summary>
        /// <param name="entity">要创建的实体</param>
        /// <returns></returns>
        void Add(T entity);

        /// <summary>
        /// 添加一个实体[异步]
        /// </summary>
        /// <param name="entity">要创建的实体</param>
        /// <returns></returns>
        Task AddAsync(T entity);

        /// <summary>
        /// 批量添加实体
        /// </summary>
        /// <param name="entityList">要创建的实体集合</param>
        /// <returns></returns>
        void Add(List<T> entityList);

        /// <summary>
        /// 批量添加实体[异步]
        /// </summary>
        /// <param name="entityList">要创建的实体集合</param>
        /// <returns></returns>
        Task AddAsync(List<T> entityList);

        /// <summary>
        /// 修改一个实体
        /// </summary>
        /// <param name="entity">要修改的实体</param>
        /// <returns></returns>
        void Update(T entity);

        /// <summary>
        /// 批量修改实体
        /// </summary>
        /// <param name="entityList">要修改的实体集合</param>
        /// <returns></returns>
        void Update(List<T> entityList);

        /// <summary>
        /// 删除一个实体
        /// </summary>
        /// <param name="entity">要删除的实体</param>
        /// <returns></returns>
        void Delete(T entity);

        /// <summary>
        /// 批量删除实体
        /// </summary>
        /// <param name="entityList">要删除的实体集合</param>
        /// <returns></returns>
        void Delete(List<T> entityList);

        /// <summary>
        /// 根据条件和排序获取一个指定列的新实体
        /// </summary>
        /// <typeparam name="R">结果实体</typeparam>
        /// <param name="selector">指定列</param>
        /// <param name="predicate">条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="disableTracking">禁用追踪</param>
        /// <returns></returns>
        R GetSingle<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true) where R : class;

        /// <summary>
        /// 根据条件和排序获取一个当前实体
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="disableTracking">禁用追踪</param>
        /// <returns></returns>
        T GetSingleCurrent(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true);

        /// <summary>
        /// 根据条件和排序获取一个指定列的新实体[异步]
        /// </summary>
        /// <typeparam name="R">结果实体</typeparam>
        /// <param name="selector">指定列</param>
        /// <param name="predicate">条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="disableTracking">禁用追踪</param>
        /// <returns></returns>
        Task<R> GetSingleAsync<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true) where R : class;

        /// <summary>
        /// 根据条件和排序获取一个当前实体[异步]
        /// </summary>
        /// <param name="predicate">条件</param>
        /// <param name="orderBy">排序</param>
        /// <param name="disableTracking">禁用追踪</param>
        /// <returns></returns>
        Task<T> GetSingleAsyncCurrent(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true);

        /// <summary>
        /// 根据条件和排序分页获取指定列实体集合
        /// </summary>
        /// <typeparam name="R"></typeparam>
        /// <param name="selector">查询的指定列</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="disableTracking">禁用追踪</param>
        /// <returns></returns>
        PageResponse<R> GetPagedList<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageIndex = PageRequest.DefaultPageIndex, int pageSize = PageRequest.DefaultPageSize, bool disableTracking = true) where R : class;

        /// <summary>
        /// 根据条件和排序分页获取当前实体集合
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="disableTracking">禁用追踪</param>
        /// <returns></returns>
        PageResponse<T> GetPagedListCurrent(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageIndex = PageRequest.DefaultPageIndex, int pageSize = PageRequest.DefaultPageSize, bool disableTracking = true);

        /// <summary>
        /// 根据条件和排序分页获取指定列实体集合[异步]
        /// </summary>
        /// <typeparam name="R">结果实体</typeparam>
        /// <param name="selector">查询的指定列</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="disableTracking">禁用追踪</param>
        /// <returns></returns>
        Task<PageResponse<R>> GetPagedListAsync<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageIndex = PageRequest.DefaultPageIndex, int pageSize = PageRequest.DefaultPageSize, bool disableTracking = true) where R : class;

        /// <summary>
        /// 根据条件和排序分页获取当前实体集合[异步]
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="disableTracking">禁用追踪</param>
        /// <returns></returns>
        Task<PageResponse<T>> GetPagedListAsyncCurrent(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageIndex = PageRequest.DefaultPageIndex, int pageSize = PageRequest.DefaultPageSize, bool disableTracking = true);

        /// <summary>
        /// 根据条件和排序获取指定列的实体集合
        /// </summary>
        /// <typeparam name="R">结果实体</typeparam>
        /// <param name="selector">查询的指定列</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <param name="disableTracking">禁用追踪</param>
        /// <returns></returns>
        List<R> GetList<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true) where R : class;

        /// <summary>
        /// 根据条件和排序获取当前实体集合
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <param name="disableTracking">禁用追踪</param>
        /// <returns></returns>
        List<T> GetListCurrent(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true);

        /// <summary>
        /// 根据条件和排序获取指定列实体集合[异步]
        /// </summary>
        /// <typeparam name="R">结果实体</typeparam>
        /// <param name="selector">查询的指定列</param>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <param name="disableTracking">禁用追踪</param>
        /// <returns></returns>
        Task<List<R>> GetListAsync<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true) where R : class;

        /// <summary>
        /// 根据条件和排序获取当前实体集合[异步]
        /// </summary>
        /// <param name="predicate">查询条件</param>
        /// <param name="orderBy">排序规则</param>
        /// <param name="disableTracking">禁用追踪</param>
        /// <returns></returns>
        Task<List<T>> GetListAsyncCurrent(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true);
    }
}
