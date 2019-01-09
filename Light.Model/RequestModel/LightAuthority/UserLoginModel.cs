using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.RequestModel.LightAuthority
{
    /// <summary>
    /// 登录请求实体
    /// </summary>
    public class UserLoginModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
