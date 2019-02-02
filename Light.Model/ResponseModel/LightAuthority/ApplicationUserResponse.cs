using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.ResponseModel.LightAuthority
{
    /// <summary>
    /// 用户显示实体
    /// </summary>
    public class ApplicationUserResponse
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }
    }
}
