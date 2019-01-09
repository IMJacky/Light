using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Light.EFRespository
{
    public class UnitOfWork<TDbContext> : IUnitOfWork<TDbContext> where TDbContext : DbContext
    {
        private Dictionary<Type, object> repositories;
        private readonly TDbContext _dbContext;

        public UnitOfWork(TDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IRepository<T> GetRepository<T>() where T : class
        {
            if (repositories == null)
            {
                repositories = new Dictionary<Type, object>();
            }

            var type = typeof(T);
            if (!repositories.ContainsKey(type))
            {
                repositories[type] = new Repository<T>(_dbContext);
            }

            return (IRepository<T>)repositories[type];
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        #region IDisposable Support
        private bool disposedValue = false; // 要检测冗余调用

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (repositories != null)
                    {
                        repositories.Clear();
                    }
                    _dbContext.Dispose();
                }
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
