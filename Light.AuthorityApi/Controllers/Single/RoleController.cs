using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Light.Model.TableModel.LightAuthority;
using Light.EFRespository;
using Light.Model.CommonModel;
using Light.EFRespository.LightAuthority;
using Light.Extension;
using Microsoft.AspNetCore.Authorization;
using Light.Common;

namespace Light.AuthorityApi.Controllers.Single
{
    /// <summary>
    /// 角色控制器
    /// </summary>
    [Produces("application/json")]
    [Route("auth/role")]
    public class RoleController : BaseController
    {
        private readonly IUnitOfWork<LightAuthorityContext> _unitOfWork;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        public RoleController(IUnitOfWork<LightAuthorityContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Role
        /// <summary>
        /// 所有角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        //[Authorize]
        public async Task<IActionResult> GetAllRole()
        {
            return Ok(await _unitOfWork.GetRepository<Role>().GetListAsyncCurrent());
        }

        /// <summary>
        /// 分页所有角色列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("page")]
        public async Task<IActionResult> GetAllUserByPage(PageRequest pageRequest)
        {
            return Ok(await _unitOfWork.GetRepository<Role>().GetPagedListAsyncCurrent(pageIndex: pageRequest.PageIndex, pageSize: pageRequest.PageSize));
        }

        /// <summary>
        /// 根据Id获取一个角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetRoleById(int id)
        {
            return Ok(await _unitOfWork.GetRepository<Role>().GetSingleAsyncCurrent(m => m.Id == id));
        }

        /// <summary>
        /// 添加一个角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddRole([FromBody]Role role)
        {
            await _unitOfWork.GetRepository<Role>().AddAsync(role);
            return Ok(await _unitOfWork.SaveChangesAsync());
        }

        /// <summary>
        /// 更新一个角色
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateRole([FromBody]Role role)
        {
            var roleExist = await _unitOfWork.GetRepository<Role>().GetSingleAsyncCurrent(m => m.Id == role.Id);
            if (roleExist == null)
            {
                return NotFound();
            }
            _unitOfWork.GetRepository<Role>().Update(role);
            return Ok(await _unitOfWork.SaveChangesAsync());
        }

        /// <summary>
        /// 删除一个角色
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            var roleExist = await _unitOfWork.GetRepository<Role>().GetSingleAsyncCurrent(m => m.Id == id);
            if (roleExist == null)
            {
                return NotFound();
            }
            _unitOfWork.GetRepository<Role>().Delete(roleExist);
            return Ok(await _unitOfWork.SaveChangesAsync());
        }

        /// <summary>
        /// 测试
        /// </summary>
        /// <returns></returns>
        [HttpGet("test")]
        public IActionResult Test()
        {
            List<Role> roles = new List<Role>();
            List<Task> tasks = new List<Task>();
            var taskOne = Task.Run(async () =>
            {
                roles.Add(await _unitOfWork.GetRepository<Role>().GetSingleAsyncCurrent(m => m.Id == 1));
            });
            tasks.Add(taskOne);
            var taskTwo = Task.Run(async () =>
            {
                roles.Add(await _unitOfWork.GetRepository<Role>().GetSingleAsyncCurrent(m => m.Id == 2));
            });
            tasks.Add(taskTwo);
            Task.WaitAll(tasks.ToArray());

            //List<ApplicationUser> applicationUsers = new List<ApplicationUser>();
            //List<int> list = new List<int>();
            //Parallel.For(1, 10, (i) =>
            //{
            //    list.Add(i);
            //});
            var aaa = RuntimeHelper.EnvironmentName;
            return Ok(roles);
        }
        #endregion
    }
}