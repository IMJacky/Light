using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.CommonModel
{
    /// <summary>
    /// 没有结果集的实体定义（只包含是否成功和消息提示）
    /// </summary>
    public class ResultNoneModel
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// 消息提示
        /// </summary>
        public string Message { get; set; }
    }
}
