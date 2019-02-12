using Light.Model.TableModel.LightBlog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Light.EFRespository.LightBlog
{
    public class LightBlogDataSeed
    {
        /// <summary>
        /// 初始化数据库数据
        /// </summary>
        /// <param name="lightContext"></param>
        /// <param name="unitOfWork"></param>
        /// <returns></returns>
        public static async Task SeedAsync(LightBlogContext lightContext, IUnitOfWork<LightBlogContext> unitOfWork)
        {
            var blogRepository = unitOfWork.GetRepository<Blog>();

            if (lightContext.Database.EnsureCreated())
            {
                blogRepository.Add(new List<Blog>{
                new Blog
                {
                    Title = "博客主标题",
                    SubTitle = "博客副标题",
                    Content = "博客内容",
                    UpdaterId = 1,
                    UpdaterName = "王杰光",
                    UpdateDate = DateTime.Now,
                    CreaterId = 1,
                    CreaterName = "王杰光",
                    CreateDate = DateTime.Now
                }});
                await unitOfWork.SaveChangesAsync();
            }
        }
    }
}
