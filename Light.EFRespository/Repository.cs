using Light.Model.CommonModel;
using Light.Model.TableModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Light.EFRespository
{
    /// <summary>
    /// EF 实现仓储接口
    /// </summary>
    /// <typeparam name="T">实体</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Add(List<T> entityList)
        {
            _dbSet.AddRange(entityList);
        }

        public ValueTask<EntityEntry<T>> AddAsync(T entity)
        {
            return _dbSet.AddAsync(entity);
        }

        public Task AddAsync(List<T> entityList)
        {
            return _dbSet.AddRangeAsync(entityList);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Delete(List<T> entityList)
        {
            _dbSet.RemoveRange(entityList);
        }

        public List<R> GetList<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true) where R : class
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                return orderBy(query).Select(selector).ToList();
            }
            else
            {
                return query.Select(selector).ToList();
            }
        }

        public List<T> GetListCurrent(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public async Task<List<R>> GetListAsync<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true) where R : class
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).ToListAsync();
            }
            else
            {
                return await query.Select(selector).ToListAsync();
            }
        }

        public async Task<List<T>> GetListAsyncCurrent(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }

        public PageResponse<R> GetPagedList<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageIndex = PageRequest.DefaultPageIndex, int pageSize = PageRequest.DefaultPageSize, bool disableTracking = true) where R : class
        {
            PageResponse<R> pageResponse = new PageResponse<R>();
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            pageResponse.TotalCount = query.Count();
            if (orderBy != null)
            {
                pageResponse.ResultList = orderBy(query).Select(selector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                pageResponse.ResultList = query.Select(selector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            return pageResponse;
        }

        public PageResponse<T> GetPagedListCurrent(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageIndex = PageRequest.DefaultPageIndex, int pageSize = PageRequest.DefaultPageSize, bool disableTracking = true)
        {
            PageResponse<T> pageResponse = new PageResponse<T>();
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            pageResponse.TotalCount = query.Count();
            if (orderBy != null)
            {
                pageResponse.ResultList = orderBy(query).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                pageResponse.ResultList = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            return pageResponse;
        }

        public async Task<PageResponse<R>> GetPagedListAsync<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageIndex = PageRequest.DefaultPageIndex, int pageSize = PageRequest.DefaultPageSize, bool disableTracking = true) where R : class
        {
            PageResponse<R> pageResponse = new PageResponse<R>();
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            pageResponse.TotalCount = await query.CountAsync();
            if (orderBy != null)
            {
                pageResponse.ResultList = await orderBy(query).Select(selector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                pageResponse.ResultList = await query.Select(selector).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            return pageResponse;
        }

        public async Task<PageResponse<T>> GetPagedListAsyncCurrent(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, int pageIndex = PageRequest.DefaultPageIndex, int pageSize = PageRequest.DefaultPageSize, bool disableTracking = true)
        {
            PageResponse<T> pageResponse = new PageResponse<T>();
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            pageResponse.TotalCount = await query.CountAsync();
            if (orderBy != null)
            {
                pageResponse.ResultList = await orderBy(query).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                pageResponse.ResultList = await query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            return pageResponse;
        }

        public R GetSingle<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true) where R : class
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).Select(selector).FirstOrDefault();
            }
            else
            {
                return query.Select(selector).FirstOrDefault();
            }
        }

        public T GetSingleCurrent(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return orderBy(query).FirstOrDefault();
            }
            else
            {
                return query.FirstOrDefault();
            }
        }

        public async Task<R> GetSingleAsync<R>(Expression<Func<T, R>> selector, Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true) where R : class
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).Select(selector).FirstOrDefaultAsync();
            }
            else
            {
                return await query.Select(selector).FirstOrDefaultAsync();
            }
        }

        public async Task<T> GetSingleAsyncCurrent(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbSet;
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).FirstOrDefaultAsync();
            }
            else
            {
                return await query.FirstOrDefaultAsync();
            }
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Update(List<T> entityList)
        {
            _dbSet.UpdateRange(entityList);
        }
    }
}
