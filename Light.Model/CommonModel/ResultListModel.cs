using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.CommonModel
{
    /// <summary>
    /// 结果对象集合
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ResultListModel<T> : ResultNoneModel where T : class
    {
        /// <summary>
        /// 结果对象集合
        /// </summary>
        public List<T> ResultList { get; set; }
    }
}
