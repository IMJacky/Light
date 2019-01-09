using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.TableModel.LightAuthority
{
    /// <summary>
    /// 角色模块对照关系
    /// </summary>
    public class RoleModule : BaseModel
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }

        /// <summary>
        /// 系统模块Id
        /// </summary>
        public long ModuleId { get; set; }
    }
}
