using Light.Model.CommonModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.RequestModel.LightAuthority
{
    /// <summary>
    /// 退出请求实体
    /// </summary>
    public class UserLogoutRequest : ResultNoneModel
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }
    }
}
