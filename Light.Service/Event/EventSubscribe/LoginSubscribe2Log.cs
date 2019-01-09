using Light.Common;
using Light.Service.Event.CustomerEventArgs;
using System;

namespace Light.Service.Event.EventSubscribe
{
    /// <summary>
    /// 登陆记录日志
    /// </summary>
    public class LoginSubscribe2Log : Subscribe<LoginEventArgs>
    {
        public override void ShouldDoWork(LoginEventArgs eventAgrs)
        {
            NLogManager.LogInfo($"记录日志：{eventAgrs.UserName}于{DateTime.Now.ToStringDefault()}登录成功，IP地址：{eventAgrs.IP}！");
        }
    }
}
