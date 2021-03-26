using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
    public static class DecimalExtension
    {
        /// <summary>
        /// 四舍五入(默认保留2位小数)
        /// </summary>
        public static decimal ToRound(this decimal val)
        {
            return decimal.Round(val, 2);
        }
        /// <summary>
        /// 四舍五入
        /// </summary>
        /// <param name="val">当前值</param>
        /// <param name="decimals">保留小数位数</param>
        public static decimal ToRound(this decimal val, int decimals)
        {
            return decimal.Round(val, decimals);
        }

        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="dividend">被除数</param>
        /// <param name="val">除数</param>
        /// <returns>返回一个正数<para>当被除数为零（0）时返回0</para></returns>
        public static decimal DivisionAbs(this decimal val, decimal dividend)
        {
            if (dividend == 0)
            {
                return 0;
            }
            return Math.Abs((val / dividend).ToRound());
        }

        /// <summary>
        /// 除以 百分比 将会乘以100
        /// </summary>
        /// <param name="dividend">被除数</param>
        /// <param name="val">除数</param>
        /// <returns>返回一个正数<para>当被除数为零（0）时返回0</para></returns>
        public static decimal DivisionRadioAbs(this decimal val, decimal dividend)
        {
            if (dividend == 0)
            {
                return 0;
            }
            return Math.Abs((val / dividend * 100).ToRound());
        }


        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="dividend">被除数</param>
        /// <param name="val">除数</param>
        /// <returns>当被除数为零（0）时返回0</returns>
        public static decimal Division(this decimal val, decimal dividend)
        {
            if (dividend == 0)
            {
                return 0;
            }
            return (val / dividend).ToRound();
        }

        /// <summary>
        /// 空值转换为0
        /// </summary>
        /// <param name="val">值</param>
        /// <returns></returns>
        public static decimal ToZero(this decimal? val)
        {
            return val??0;
        }
    }
}
