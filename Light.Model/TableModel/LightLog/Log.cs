using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.TableModel.LightLog
{
    /// <summary>
    /// 日志实体
    /// </summary>
    public class Log
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 日志信息
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public string Level { get; set; }
    }
}
