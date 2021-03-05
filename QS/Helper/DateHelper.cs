using System;

namespace QS.Helper
{
    /// <summary>
    /// 日期帮助类
    /// </summary>
    public class DateHelper
    {

        /// <summary>
        /// 获取相差月份 正整数
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public static int GetDiffMonth(DateTime startDate, DateTime endDate)
        {
            return Math.Abs((endDate.Year - startDate.Year) * 12 + (endDate.Month - startDate.Month));
        }

        /// <summary>
        /// 根据月份获取日期
        /// </summary>
        /// <param name="month">月份</param>
        /// <returns></returns>
        public static DateTime GetDateTimeByMonth(int month)
        {
            return GetDateTime(DateTime.Now.Year, month);
        }

        /// <summary>
        /// 根据年月日获取指定日期
        /// <para>默认1号</para>
        /// </summary>
        /// <param name="day">日</param>
        /// <param name="month">月</param>
        /// <param name="year">年</param>
        /// <returns></returns>
        public static DateTime GetDateTime(int year,int month,int day = 1)
        {
            DateTime.TryParse($"{year}/{month}/{day}", out DateTime startTime);
            return startTime;
        }
    }
}
