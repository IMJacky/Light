using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.TableModel
{
    /// <summary>
    /// 角色实体
    /// </summary>
    public class Role : BaseModel
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// 一个角色包含多个用户
        /// </summary>
        public ICollection<User> UserList { get; set; }
    }
}
