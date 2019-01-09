using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.ViewModel.LightAuthority
{
    /// <summary>
    /// 用户权限详情
    /// </summary>
    public class UserAuthorityDetail
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 用户的角色列表(可能会有多个角色)
        /// </summary>
        public List<string> RoleNameList { get; set; }

        /// <summary>
        /// 用户角色所有的系统模块详情
        /// </summary>
        public List<UserModuleDetail> UserModuleDetailList { get; set; }
    }

    /// <summary>
    /// 角色所拥有的系统模块的权限详情
    /// </summary>
    public class UserModuleDetail
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        public long ModuleId { get; set; }

        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// Angular的路由地址
        /// </summary>
        public string RouteUrl { get; set; }

        /// <summary>
        /// 父级Id（Id=0表示头部总菜单）
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 排序值（越大越靠前）
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 用户角色所有的系统子模块详情（可以无限递归）
        /// </summary>
        public List<UserModuleDetail> SubUserModuleDetailList { get; set; }
    }
}
