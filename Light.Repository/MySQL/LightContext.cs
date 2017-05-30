using Light.Model.TableModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Repository.MySQL
{
    /// <summary>
    /// MySQL数据库访问上下文
    /// </summary>
    public class LightContext : DbContext
    {
        public LightContext(DbContextOptions<LightContext> options)
            : base(options)
        {
        }

        public DbSet<User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(m =>
            {
                m.Property(n => n.UserName).HasMaxLength(50);//设置用户名最大长度为50个字符
                m.Property(n => n.Password).HasMaxLength(20).IsRequired();//设置密码不可空且最大20个字符
            });
            base.OnModelCreating(modelBuilder);
        }
    }
}
