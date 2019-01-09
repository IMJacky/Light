using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;

namespace Light.Common
{
    public static class ExtendMethod
    {
        /// <summary>
        /// 获取枚举字段上的属性
        /// </summary>
        /// <typeparam name="T">属性</typeparam>
        /// <param name="e">枚举</param>
        /// <returns></returns>
        private static T GetAttributeOfType<T>(this Enum e) where T : Attribute
        {
            var type = e.GetType();
            var memInfo = type.GetMember(e.ToString());
            var attributes = memInfo[0].GetCustomAttributes(typeof(T), false);
            return (attributes.Length > 0) ? (T)attributes[0] : null;
        }

        public static string GetDescription(this Enum e)
        {
            return e.GetAttributeOfType<DescriptionAttribute>().Description;
        }

        /// <summary>
        /// 转换时间格式：yyyy-MM-dd HH:mm:ss
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static string ToStringDefault(this DateTime dateTime)
        {
            return dateTime.ToString("yyyy-MM-dd HH:mm:ss");
        }
    }
}
