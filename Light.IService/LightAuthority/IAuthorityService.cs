using Light.Model.CommonModel;
using Light.Model.ViewModel.LightAuthority;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Light.IService.LightAuthority
{
    /// <summary>
    /// 权限相关服务接口定义
    /// </summary>
    public interface IAuthorityService
    {
        /// <summary>
        /// 获取用户所有的权限相关信息
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <returns></returns>
        Task<ResultModel<UserAuthorityDetail>> GetUserAuthorityDetail(int userId);
    }
}
