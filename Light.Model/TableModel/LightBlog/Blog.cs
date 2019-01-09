using System;
using System.Collections.Generic;
using System.Text;

namespace Light.Model.TableModel.LightBlog
{
    /// <summary>
    /// 博客主表实体
    /// </summary>
    public class Blog : BaseModel
    {
        /// <summary>
        /// 主标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 博客内容
        /// </summary>
        public string Content { get; set; }
    }
}
