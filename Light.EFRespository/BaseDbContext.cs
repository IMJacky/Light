using Light.Model.TableModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Light.EFRespository
{
    public class BaseDbContext<T> : DbContext where T : DbContext
    {
        public BaseDbContext(DbContextOptions<T> options) : base(options)
        {
        }

        /// <summary>
        /// Light.Model.dll 下的实体文件夹即命名空间，Light.Model.dll也可以用参数传递
        /// </summary>
        protected string ModelNameSpace;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (string.IsNullOrWhiteSpace(ModelNameSpace))
                throw new Exception("ModelNameSpace can't empty");
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Light.Model.dll");
            Assembly assembly = Assembly.LoadFrom(path);
            Type[] types = assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsSubclassOf(typeof(BaseModel)) && ModelNameSpace == type.Namespace && modelBuilder.Model.GetEntityTypes(type).Count == 0)
                {
                    modelBuilder.Model.AddEntityType(type);
                }
            }
            foreach (IMutableEntityType entityType in modelBuilder.Model.GetEntityTypes())
            {
                var createDateProperty = entityType.FindProperty("CreateDate");
                if (createDateProperty != null && createDateProperty.ClrType == typeof(DateTime))
                {
                    createDateProperty.SqlServer().ColumnType = "datetime";
                }

                var updateDateProperty = entityType.FindProperty("UpdateDate");
                if (updateDateProperty != null && updateDateProperty.ClrType == typeof(DateTime))
                {
                    updateDateProperty.SqlServer().ColumnType = "datetime";
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreateDate").CurrentValue = DateTime.Now;
                    entry.Property("UpdateDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdateDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Deleted)//设置成软删除
                {
                    entry.State = EntityState.Modified;

                    entry.Property("UpdateDate").CurrentValue = DateTime.Now;
                    entry.CurrentValues["IsDeleted"] = true;
                }
            }

            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("CreateDate").CurrentValue = DateTime.Now;
                    entry.Property("UpdateDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("UpdateDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Deleted)
                {
                    entry.State = EntityState.Modified;

                    entry.Property("UpdateDate").CurrentValue = DateTime.Now;
                    entry.CurrentValues["IsDeleted"] = true;
                }
            }

            return await base.SaveChangesAsync();
        }
    }
}
