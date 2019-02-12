using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Light.Extension;
using Light.IService.LightAuthority;
using Light.Model.CommonModel;
using Light.Model.ResponseModel.LightAuthority;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Light.AuthorityApi.Controllers.Multi
{
    /// <summary>
    /// 用户权限相关控制器
    /// </summary>
    [Produces("application/json")]
    [Route("auth/detail")]
    public class AuthorityController : BaseController
    {
        private readonly IAuthorityService iAuthorityService;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="authorityService"></param>
        public AuthorityController(IAuthorityService authorityService)
        {
            iAuthorityService = authorityService;
        }

        /// <summary>
        /// 获取用户所有的权限信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        [HttpGet("usermodule")]
        public async Task<IActionResult> GetUserAuthorityDetailAsync(int userId)
        {
            return Ok(await iAuthorityService.GetUserAuthorityDetail(userId));
        }
    }
}