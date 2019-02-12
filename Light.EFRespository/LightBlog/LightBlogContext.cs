using Microsoft.EntityFrameworkCore;

namespace Light.EFRespository.LightBlog
{
    /// <summary>
    /// 博客上下文
    /// </summary>
    public class LightBlogContext : BaseDbContext<LightBlogContext>
    {
        public LightBlogContext(DbContextOptions<LightBlogContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ModelNameSpace = "Light.Model.TableModel.LightBlog";
            LightBlogModelBind.ConfigModel(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }
    }
}
