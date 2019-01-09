using Light.Service.Event.CustomerEventArgs;
using Light.Service.Event.EventSubscribe;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Light.Service.Event.EventPublisher
{
    /// <summary>
    /// 登录时的发布者（可以废弃了，改用泛型）
    /// </summary>
    public class LoginPublisher : Publisher<LoginEventArgs>
    {

        private List<Subscribe<LoginEventArgs>> _SubscribesList;

        public LoginPublisher()
        {
            _SubscribesList = EventBus.GetSubscribes<LoginEventArgs>();
            //SubscribesList.Add(new LoginSubscribe2Log());
            //SubscribesList.Add(new LoginSubscribe2Message());
        }

        public override void Notify(LoginEventArgs eventAgrs)
        {
            foreach (var subscribe in _SubscribesList)
            {
                subscribe.ShouldDoWork(eventAgrs);
            }
        }
    }
}
