using Light.Model.TableModel;
using Light.Model.TableModel.LightAuthority;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Light.EFRespository.LightAuthority
{
    /// <summary>
    /// 系统上下文
    /// </summary>
    public class LightAuthorityContext : BaseDbContext<LightAuthorityContext>
    {
        //public bool? IsDeleted { get; private set; } //私有化IsDeleted全局查询

        //public LightContext(DbContextOptions<LightContext> options, bool? isDeleted = false) : base(options)
        //{
        //    IsDeleted = isDeleted;
        //}

        public LightAuthorityContext(DbContextOptions<LightAuthorityContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelNameSpace = "Light.Model.TableModel.LightAuthority";
            LightAuthorityModelBind.ConfigModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
