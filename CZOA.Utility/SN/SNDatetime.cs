using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SN.Utility
{
    /// <summary>
    /// 获取时间字符串
    /// </summary>
    public class SNDateTime
    {
        /// <summary>
        /// 返回例如:2014-01-01 23:59:59.001的时间字符串
        /// </summary>
        public static string GetDateTimeString(DateTime dt, string format)
        {
            var re = dt.ToString(format);
            return re;
        }
    }
    /// <summary>
    /// 时间格式字符串
    /// </summary>
    public class SNDateTimeFormat
    {
        /// <summary>
        /// 20140101235959001
        /// yyyyMMddHHmmssfff
        /// </summary>
        public static string _110 = "yyyyMMddHHmmssfff";
        /// <summary>
        /// 2014-01-01 23:59:59.001
        /// yyyy-MM-dd HH:mm:ss.fff
        /// </summary>
        public static string _100 = "yyyy-MM-dd HH:mm:ss.fff";
        /// <summary>
        /// 2014-01-01 23:59:59
        /// yyyy-MM-dd HH:mm:ss
        /// </summary>
        public static string _90 = "yyyy-MM-dd HH:mm:ss";
        /// <summary>
        /// 2014-1-1 9:1:59
        /// yyyy-M-d h:m:s
        /// </summary>
        public static string _95 = "yyyy-M-d h:m:s";
        /// <summary>
        /// 2014-01-01
        /// yyyy-MM-dd
        /// </summary>
        public static string _80 = "yyyy-MM-dd";
        /// <summary>
        /// 2014-1-1
        /// yyyy-M-d
        /// </summary>
        public static string _85 = "yyyy-M-d";
    }
}
