using Microsoft.EntityFrameworkCore;

namespace Light.EFRespository.LightLog
{
    /// <summary>
    /// 日志上下文
    /// </summary>
    public class LightLogContext : BaseDbContext<LightLogContext>
    {
        public LightLogContext(DbContextOptions<LightLogContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelNameSpace = "Light.Model.TableModel.LightLog";
            LightLogModelBind.ConfigModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
