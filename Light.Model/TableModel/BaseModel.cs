using System;

namespace Light.Model.TableModel
{
    /// <summary>
    /// 基类表实体
    /// </summary>
    public class BaseModel
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        public int UpdateUserId { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 标志是否删除（0未删除，1已删除）
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
