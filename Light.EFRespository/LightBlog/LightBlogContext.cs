using Microsoft.EntityFrameworkCore;

namespace Light.EFRespository.LightBlog
{
    /// <summary>
    /// 系统上下文
    /// </summary>
    public class LightBlogContext : BaseDbContext<LightBlogContext>
    {
        //public bool? IsDeleted { get; private set; } //私有化IsDeleted全局查询

        //public LightContext(DbContextOptions<LightContext> options, bool? isDeleted = false) : base(options)
        //{
        //    IsDeleted = isDeleted;
        //}

        public LightBlogContext(DbContextOptions<LightBlogContext> options) : base(options)
        {
        }
    }
}
