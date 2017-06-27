using System;
using System.Collections.Generic;

namespace Light.Model.TableModel
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class User : BaseModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 性别（0女，1男）
        /// </summary>
        public bool Gender { get; set; }

        /// <summary>
        /// 出生年月日
        /// </summary>
        public DateTime Birthday { get; set; }

        /// <summary>
        /// 一个用户隶属于多个角色
        /// </summary>
        //public ICollection<Role> RoleList { get; set; }

        ///// <summary>
        ///// 邮件地址
        ///// </summary>
        //public string Email { get; set; }
    }
}
