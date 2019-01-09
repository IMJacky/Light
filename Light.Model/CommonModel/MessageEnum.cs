using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Light.Model.CommonModel
{
    /// <summary>
    /// 消息类型枚举
    /// </summary>
    public class MessageEnum
    {
        /// <summary>
        /// 成功或者失败枚举
        /// </summary>
        public enum SuccessEnum
        {
            /// <summary>
            /// 成功
            /// </summary>
            [Description("成功")]
            Success,

            /// <summary>
            /// 失败
            /// </summary>
            [Description("失败")]
            Failure
        }

        /// <summary>
        /// 用户权限相关
        /// </summary>
        public enum UserAuthority
        {
            /// <summary>
            /// 成功
            /// </summary>
            [Description("成功")]
            Success,

            /// <summary>
            /// 当前用户不存在
            /// </summary>
            [Description("当前用户不存在")]
            UserIsNotValid,

            /// <summary>
            /// 当前用户没有任何角色
            /// </summary>
            [Description("当前用户没有任何角色")]
            UserNoRole,

            /// <summary>
            /// 用户所属角色不存在
            /// </summary>
            [Description("用户所属角色不存在")]
            RoleIsNotValid,

            /// <summary>
            /// 所属角色没有配置系统模块
            /// </summary>
            [Description("所属角色没有配置系统模块")]
            RoleNotHasModule,

            /// <summary>
            /// 系统模块不存在
            /// </summary>
            [Description("系统模块不存在")]
            ModuleIsNotValid
        }

        /// <summary>
        /// 用户相关
        /// </summary>
        public enum UserMessageEnum
        {
            /// <summary>
            /// 登录成功
            /// </summary>
            [Description("登录成功")]
            LoginSuccess,

            /// <summary>
            /// 用户名或密码错误
            /// </summary>
            [Description("用户名或密码错误")]
            UserNameOrPasswordError,
        }
    }
}
