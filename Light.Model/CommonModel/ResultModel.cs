using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.CommonModel
{
    /// <summary>
    /// 单个结果对象
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultModel<T> : ResultNoneModel where T : class, new()
    {
        /// <summary>
        /// 单个结果对象
        /// </summary>
        public T Result { get; set; } = new T();
    }
}
