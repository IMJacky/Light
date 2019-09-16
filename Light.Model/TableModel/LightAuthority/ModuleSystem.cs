using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.TableModel.LightAuthority
{
    /// <summary>
    /// 系统模块
    /// </summary>
    public class ModuleSystem : BaseModel
    {
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get; set; }

        /// <summary>
        /// 路由地址
        /// </summary>
        public string RouteUrl { get; set; }

        /// <summary>
        /// 父级Id（Id=0表示头部总菜单）
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 排序值（越大越靠前）
        /// </summary>
        public int Sort { get; set; }
    }
}
