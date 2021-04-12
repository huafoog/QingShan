using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Extensions.Object
{
    public static class DateTimeExtension
    {
        #region string转日期
        /// <summary>
        /// 返回10位时间戳 Timestamp
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static long ToUnixTimestamp(this DateTime target)
        {
            if (target.Kind == DateTimeKind.Unspecified)
                target = target.ToLocalTime();
            return (long)((target.ToUniversalTime().Ticks - 621355968000000000) / 10000000);
        }

        /// <summary>
        /// 将10位时间戳Timestamp转换成日期
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        public static DateTime ToLocalDateTime(this long target)
        {
            var date = new DateTime(621355968000000000 + (long)target * (long)10000000, DateTimeKind.Utc);
            return date.ToLocalTime();
        }
        /// <summary>  
        /// 获取时间戳 获取13位(毫秒)时间戳
        /// </summary>  
        /// <param name="dt">当前时间</param>  
        /// <returns></returns>  
        public static long ToTimeStamp(this DateTime dt)
        {
            return dt.ToTimeStamp(false);
        }

        /// <summary>  
        /// 获取时间戳,为真时获取10位(秒)时间戳(Unix),为假时获取13位(毫秒)时间戳
        /// </summary>  
        /// <param name="dt">当前时间</param>
        /// <param name="bflag">.</param>  
        /// <returns></returns>  
        public static long ToTimeStamp(this DateTime dt, bool bflag)
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区
            TimeSpan ts = dt - startTime;
            long ret = 0;
            if (bflag)
                ret = Convert.ToInt64(ts.TotalSeconds);
            else
                ret = Convert.ToInt64(ts.TotalMilliseconds);

            return ret;
        }

        /// <summary>
        /// 将时间戳转换为DateTime时间   毫秒
        /// </summary>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        public static DateTime ToTimeStampToDateTime(this long timestamp)
        {
            return timestamp.ToTimeStampToDateTime(false);
        }


        /// <summary>
        /// 将时间戳转换为DateTime时间，bSecond为true：秒，bSecond为false：毫秒
        /// </summary>
        /// <param name="timestamp"></param>
        /// <param name="bSecond"></param>
        /// <returns></returns>
        public static DateTime ToTimeStampToDateTime(this long timestamp, bool bSecond)
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTime(new System.DateTime(1970, 1, 1), TimeZoneInfo.Local); // 当地时区

            if (bSecond)
            {
                return startTime.AddSeconds(timestamp);
            }
            else
                return startTime.AddMilliseconds(timestamp);
        }

        #endregion
    }
}
