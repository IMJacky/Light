using Light.Model.CommonModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Light.Model.RequestModel.LightAuthority
{
    /// <summary>
    /// 登录请求实体
    /// </summary>
    public class UserLoginRequest : ResultNoneModel
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("用户名")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} 不能为空")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "{0} 不能为空")]
        public string Password { get; set; }

        /// <summary>
        /// 请求的回调地址
        /// </summary>
        public string ReturnUrl { get; set; }
    }
}
