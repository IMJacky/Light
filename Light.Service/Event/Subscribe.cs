using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Service.Event
{
    /// <summary>
    /// 抽象订阅者
    /// </summary>
    /// <typeparam name="T">事件主题</typeparam>
    public abstract class Subscribe<T> where T : EventArgs
    {
        /// <summary>
        /// 订阅者需要干的事情
        /// </summary>
        /// <param name="eventAgrs">事件主题</param>
        public abstract void ShouldDoWork(T eventAgrs);
    }
}
