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
    }
}
