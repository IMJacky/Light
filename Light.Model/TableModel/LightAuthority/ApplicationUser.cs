using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Light.Model.TableModel.LightAuthority
{
    /// <summary>
    /// 系统应用的用户实体
    /// </summary>
    public class ApplicationUser : BaseModel
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
        /// 邮件地址
        /// </summary>
        public string Email { get; set; }
    }
}
