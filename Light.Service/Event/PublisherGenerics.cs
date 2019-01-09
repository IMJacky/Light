using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Service.Event
{
    /// <summary>
    /// 订阅者泛型
    /// </summary>
    public class PublisherGenerics<T> : Publisher<T> where T : EventArgs
    {
        private List<Subscribe<T>> _SubscribesList;

        public PublisherGenerics()
        {
            _SubscribesList = EventBus.GetSubscribes<T>();
            //SubscribesList.Add(new LoginSubscribe2Log());
            //SubscribesList.Add(new LoginSubscribe2Message());
        }

        public override void Notify(T eventAgrs)
        {
            foreach (var subscribe in _SubscribesList)
            {
                subscribe.ShouldDoWork(eventAgrs);
            }
        }
    }
}
