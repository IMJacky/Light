using Light.Common;
using Light.Service.Event.CustomerEventArgs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Service.Event.EventSubscribe
{
    public class RegisterSubscribe2Log : Subscribe<RegisterEventArgs>
    {
        public override void ShouldDoWork(RegisterEventArgs eventAgrs)
        {
            NLogManager.LogInfo($"记录日志：{eventAgrs.UserName}于{DateTime.Now.ToStringDefault()}注册成功，IP地址：{eventAgrs.IP}！");
        }
    }
}
