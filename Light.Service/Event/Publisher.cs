using System;
using System.Collections.Generic;

namespace Light.Service.Event
{
    /// <summary>
    /// 发布者方法实现
    /// </summary>
    /// <typeparam name="T">事件主题</typeparam>
    public abstract class Publisher<T> where T : EventArgs
    {
        public abstract void Notify(T eventAgrs);
    }
}
