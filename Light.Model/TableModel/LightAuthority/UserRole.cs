using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.TableModel.LightAuthority
{
    /// <summary>
    /// 用户角色对照实体
    /// </summary>
    public class UserRole : BaseModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }
    }
}
