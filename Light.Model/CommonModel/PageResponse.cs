using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.CommonModel
{
    /// <summary>
    /// 分页的响应实体
    /// </summary>
    /// <typeparam name="R"></typeparam>
    public class PageResponse<R> where R : class
    {
        /// <summary>
        /// 总数量
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// 结果对象集合
        /// </summary>
        public List<R> ResultList { get; set; }
    }
}
