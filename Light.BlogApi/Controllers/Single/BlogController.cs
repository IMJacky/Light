using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Light.EFRespository;
using Light.EFRespository.LightBlog;
using Light.Extension;
using Light.Model.CommonModel;
using Light.Model.TableModel.LightBlog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Light.BlogApi.Controllers.Single
{
    /// <summary>
    /// 博客控制器
    /// </summary>
    [Produces("application/json")]
    [Route("blog/main")]
    public class BlogController : BaseController
    {
        private readonly IUnitOfWork<LightBlogContext> _unitOfWork;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        public BlogController(IUnitOfWork<LightBlogContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 所有博客列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        [Authorize]
        public async Task<IActionResult> GetAllBlog()
        {
            return Ok(await _unitOfWork.GetRepository<Blog>().GetListAsyncCurrent());
        }

        /// <summary>
        /// 分页所有博客列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("page")]
        public async Task<IActionResult> GetAllUserByPage(PageRequest pageRequest)
        {
            return Ok(await _unitOfWork.GetRepository<Blog>().GetPagedListAsyncCurrent(pageIndex: pageRequest.PageIndex, pageSize: pageRequest.PageSize));
        }

        /// <summary>
        /// 根据Id获取一个博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            return Ok(await _unitOfWork.GetRepository<Blog>().GetSingleAsyncCurrent(m => m.Id == id));
        }

        /// <summary>
        /// 添加一个博客
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddBlog([FromBody]Blog role)
        {
            await _unitOfWork.GetRepository<Blog>().AddAsync(role);
            return Ok(await _unitOfWork.SaveChangesAsync());
        }

        /// <summary>
        /// 更新一个博客
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateBlog([FromBody]Blog role)
        {
            var roleExist = await _unitOfWork.GetRepository<Blog>().GetSingleAsyncCurrent(m => m.Id == role.Id);
            if (roleExist == null)
            {
                return NotFound();
            }
            _unitOfWork.GetRepository<Blog>().Update(role);
            return Ok(await _unitOfWork.SaveChangesAsync());
        }

        /// <summary>
        /// 删除一个博客
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteBlog(int id)
        {
            var roleExist = await _unitOfWork.GetRepository<Blog>().GetSingleAsyncCurrent(m => m.Id == id);
            if (roleExist == null)
            {
                return NotFound();
            }
            _unitOfWork.GetRepository<Blog>().Delete(roleExist);
            return Ok(await _unitOfWork.SaveChangesAsync());
        }
    }
}
