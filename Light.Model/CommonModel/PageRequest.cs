using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Light.Model.CommonModel
{
    /// <summary>
    /// 分页请求参数
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// 默认第一页
        /// </summary>
        public const int DefaultPageIndex = 1;

        /// <summary>
        /// 默认页大小为10
        /// </summary>
        public const int DefaultPageSize = 10;

        /// <summary>
        /// 当前页数
        /// </summary>
        public int PageIndex { get; set; }

        /// <summary>
        /// 每页的条数
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        public PageRequest()
        {
            PageIndex = DefaultPageIndex;
            PageSize = DefaultPageSize;
        }
    }
}
