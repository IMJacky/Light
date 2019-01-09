using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.MQModel
{
    /// <summary>
    /// 消息文本实体
    /// </summary>
    [EasyNetQ.Queue("FuckGan.Queue:Name", ExchangeName = "FuckGanExchangeName.Fuck:You")]
    public class TextMessage
    {
        /// <summary>
        /// 文本内容
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// 路由Key
        /// </summary>
        public string RoutingKey { get; set; }
    }
}
