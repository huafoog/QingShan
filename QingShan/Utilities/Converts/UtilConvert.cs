using System;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace QingShan.Utilities
{
    /// <summary>
    /// 数据类型转换
    /// </summary>
    public static class UtilConvert
    {
        /// <summary>
        /// 转换为<see cref="Int32"/>
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static int ToInt(this object thisValue)
        {
            int reval = 0;
            if (thisValue == null)
            {
                return 0;
            }

            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        /// <summary>
        /// 转换为<see cref="int"/>
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static int ToInt(this object thisValue, int errorValue)
        {
            if (thisValue != null && thisValue != DBNull.Value && int.TryParse(thisValue.ToString(), out int reval))
            {
                return reval;
            }
            return errorValue;
        }

        /// <summary>
        /// 转换为<see cref="long"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static long ToLong(this object s)
        {
            if (s == null || s == DBNull.Value)
            {
                return 0L;
            }

            long.TryParse(s.ToString(), out long result);
            return result;
        }

        /// <summary>
        /// 四舍五入保留两位小数
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="decimals">小数位数</param>
        /// <returns></returns>
        public static decimal ToMoney(this object thisValue,int decimals = 2)
        {
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out decimal reval))
            {
                return reval.ToRound(decimals);
            }
            return 0;
        }

        /// <summary>
        /// 转换为decimal 四舍五入保留两位小数
        /// </summary>
        /// <param name="thisValue">当前值</param>
        /// <param name="errorValue">转换异常返回的值</param>
        /// <param name="decimals">小数位数</param>
        /// <returns></returns>
        public static decimal ToMoney(this object thisValue, decimal errorValue, int decimals = 2)
        {
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out decimal reval))
            {
                return reval.ToRound(decimals);
            }
            return errorValue;
        }

        /// <summary>
        /// 转换为<see cref="String"/>
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static string ToString(this object thisValue)
        {
            if (thisValue != null)
            {
                return thisValue.ToString().Trim();
            }

            return "";
        }

        /// <summary>
        /// 转换为<see cref="string"/>
        /// </summary>
        /// <param name="thisValue">当前值</param>
        /// <param name="errorValue">转换异常返回的值</param>
        /// <returns></returns>
        public static string ToString(this object thisValue, string errorValue)
        {
            if (thisValue != null)
            {
                return thisValue.ToString().Trim();
            }

            return errorValue;
        }

        /// <summary>
        /// 转换成Double/Single
        /// </summary>
        /// <param name="s"></param>
        /// <param name="digits">小数位数</param>
        /// <returns></returns>
        public static double ToDouble(this object s, int? digits = null)
        {
            if (s == null || s == DBNull.Value)
            {
                return 0d;
            }

            double.TryParse(s.ToString(), out double result);

            if (digits == null)
            {
                return result;
            }

            return Math.Round(result, digits.Value);
        }

        /// <summary>
        /// 转换为<see cref="decimal"/>
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object thisValue)
        {
            if (thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out decimal reval))
            {
                return reval;
            }
            return 0;
        }

        /// <summary>
        /// 转换为<see cref="decimal"/>
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(this object thisValue, decimal errorValue)
        {
            return thisValue != null && thisValue != DBNull.Value && decimal.TryParse(thisValue.ToString(), out decimal reval)
                ? reval
                : errorValue;
        }

        /// <summary>
        /// 转换为<see cref="DateTime"/>
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static DateTime ToDate(this object thisValue)
        {
            DateTime reval = DateTime.MinValue;
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out reval))
            {
                reval = Convert.ToDateTime(thisValue);
            }
            return reval;
        }

        /// <summary>
        /// 转换为<see cref="DateTime"/>
        /// </summary>
        /// <param name="thisValue"></param>
        /// <param name="errorValue"></param>
        /// <returns></returns>
        public static DateTime ToDate(this object thisValue, DateTime errorValue)
        {
            if (thisValue != null && thisValue != DBNull.Value && DateTime.TryParse(thisValue.ToString(), out DateTime reval))
            {
                return reval;
            }
            return errorValue;
        }

        /// <summary>
        /// 转换为<see cref="bool"/>
        /// </summary>
        /// <param name="thisValue"></param>
        /// <returns></returns>
        public static bool ToBool(this object thisValue)
        {
            bool reval = false;
            if (thisValue != null && thisValue != DBNull.Value && bool.TryParse(thisValue.ToString(), out reval))
            {
                return reval;
            }
            return reval;
        }

        /// <summary>
        /// 转换为<see cref="byte"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte ToByte(this object s)
        {
            if (s == null || s == DBNull.Value)
            {
                return 0;
            }

            byte.TryParse(s.ToString(), out byte result);
            return result;
        }

        /// <summary>
        /// 转换为<see cref="byte"/>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] ToBytes(this object s)
        {
            if (s == null || s == DBNull.Value)
            {
                return Array.Empty<byte>();
            }

            var str = Newtonsoft.Json.JsonConvert.SerializeObject(s);
            UnicodeEncoding uniEncoding = new UnicodeEncoding();
            return uniEncoding.GetBytes(str);
        }

        #region ==字节转换==
        /// <summary>
        /// 转换为16进制
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="lowerCase">是否小写</param>
        /// <returns></returns>
        public static string ToHex(this byte[] bytes, bool lowerCase = true)
        {
            if (bytes == null)
            {
                return null;
            }

            var result = new StringBuilder();
            var format = lowerCase ? "x2" : "X2";
            for (var i = 0; i < bytes.Length; i++)
            {
                result.Append(bytes[i].ToString(format));
            }

            return result.ToString();
        }

        /// <summary>
        /// 16进制转字节数组
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(this string s)
        {
            if (s.IsNull())
            {
                return null;
            }

            var bytes = new byte[s.Length / 2];

            for (int x = 0; x < s.Length / 2; x++)
            {
                int i = (Convert.ToInt32(s.Substring(x * 2, 2), 16));
                bytes[x] = (byte)i;
            }

            return bytes;
        }

        /// <summary>
        /// 转换为Base64
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ToBase64(this byte[] bytes)
        {
            if (bytes == null)
            {
                return null;
            }

            return Convert.ToBase64String(bytes);
        }

        #endregion
    }
}
