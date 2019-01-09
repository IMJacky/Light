using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

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
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 创建人Id
        /// </summary>
        public long CreaterId { get; set; }

        /// <summary>
        /// 创建人姓名
        /// </summary>
        [StringLength(20)]
        [Required]
        public string CreaterName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新人Id
        /// </summary>
        public long UpdaterId { get; set; }

        /// <summary>
        /// 更新人姓名
        /// </summary>
        [StringLength(20)]
        [Required]
        public string UpdaterName { get; set; }

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
