using Light.Common;
using Light.Service.Event.CustomerEventArgs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Service.Event.EventSubscribe
{
    /// <summary>
    /// 登陆发送短信
    /// </summary>
    public class LoginSubscribe2Message : Subscribe<LoginEventArgs>
    {
        public override void ShouldDoWork(LoginEventArgs eventAgrs)
        {
            NLogManager.LogInfo($"发送短信：{eventAgrs.UserName}于{DateTime.Now.ToStringDefault()}登录成功，IP地址：{eventAgrs.IP}！");
        }
    }
}
