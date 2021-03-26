using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    /// <summary>
    /// <see cref="int"/>拓展
    /// </summary>
    public static class Int32Extension
    {
        /// <summary>
        /// 除以
        /// </summary>
        /// <param name="dividend">被除数</param>
        /// <param name="val">除数</param>
        /// <returns>当被除数为零（0）时返回0</returns>
        public static decimal Division(this int val, decimal dividend)
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
        public static int ToZero(this int? val)
        {
            return val ?? 0;
        }
    }
}
