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
using Microsoft.AspNetCore.Authorization;

namespace Light.AuthorityApi.Controllers.Single
{
    /// <summary>
    /// 系统模块控制器
    /// </summary>
    [Produces("application/json")]
    [Route("auth/moduleSystem")]
    public class ModuleSystemController : Controller
    {
        private readonly IUnitOfWork<LightAuthorityContext> _unitOfWork;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ModuleSystemController(IUnitOfWork<LightAuthorityContext> unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region ModuleSystem
        /// <summary>
        /// 所有系统模块列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("all")]
        public async Task<IActionResult> GetAllModuleSystem()
        {
            return Ok(await _unitOfWork.GetRepository<ModuleSystem>().GetListAsyncCurrent());
        }

        /// <summary>
        /// 分页所有系统模块列表
        /// </summary>
        /// <returns></returns>
        [HttpGet("page")]
        public async Task<IActionResult> GetAllUserByPage(PageRequest pageRequest)
        {
            return Ok(await _unitOfWork.GetRepository<ModuleSystem>().GetPagedListAsyncCurrent(pageIndex: pageRequest.PageIndex, pageSize: pageRequest.PageSize));
        }

        /// <summary>
        /// 根据Id获取一个系统模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("getbyid")]
        public async Task<IActionResult> GetModuleSystemById(int id)
        {
            return Ok(await _unitOfWork.GetRepository<ModuleSystem>().GetSingleAsyncCurrent(m => m.Id == id));
        }

        /// <summary>
        /// 添加一个系统模块
        /// </summary>
        /// <param name="moduleSystem"></param>
        /// <returns></returns>
        [HttpPost("add")]
        public async Task<IActionResult> AddModuleSystem([FromBody]ModuleSystem moduleSystem)
        {
            await _unitOfWork.GetRepository<ModuleSystem>().AddAsync(moduleSystem);
            return Ok(await _unitOfWork.SaveChangesAsync());
        }

        /// <summary>
        /// 更新一个系统模块
        /// </summary>
        /// <param name="moduleSystem"></param>
        /// <returns></returns>
        [HttpPut("update")]
        public async Task<IActionResult> UpdateModuleSystem([FromBody]ModuleSystem moduleSystem)
        {
            var moduleSystemExist = await _unitOfWork.GetRepository<ModuleSystem>().GetSingleAsyncCurrent(m => m.Id == moduleSystem.Id);
            if (moduleSystemExist == null)
            {
                return NotFound();
            }
            _unitOfWork.GetRepository<ModuleSystem>().Update(moduleSystem);
            return Ok(await _unitOfWork.SaveChangesAsync());
        }

        /// <summary>
        /// 删除一个系统模块
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteModuleSystem(int id)
        {
            var moduleSystemExist = await _unitOfWork.GetRepository<ModuleSystem>().GetSingleAsyncCurrent(m => m.Id == id);
            if (moduleSystemExist == null)
            {
                return NotFound();
            }
            _unitOfWork.GetRepository<ModuleSystem>().Delete(moduleSystemExist);
            return Ok(await _unitOfWork.SaveChangesAsync());
        }
        #endregion
    }
}