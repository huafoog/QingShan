using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Utilities
{
    /// <summary>
    /// Base64转换
    /// </summary>
    public class Base64Convert
    {
        private static readonly HashSet<char> _base64CodeArray = new HashSet<char>(){
        'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P',
        'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f',
        'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v',
        'w', 'x', 'y', 'z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '+', '/',
        '='};

        /// <summary>
        /// 是否base64字符串
        /// </summary>
        /// <param name="base64Str">要判断的字符串</param>
        /// <param name="bytes">字符串转换成的字节数组</param>
        /// <returns></returns>
        public static bool IsBase64(string base64Str, out byte[] bytes)
        {
            bytes = null;
            try
            {
                if (string.IsNullOrEmpty(base64Str))
                    return false;
                else
                {
                    if (base64Str.Contains(","))
                        base64Str = base64Str.Split(',')[1];
                    if (base64Str.Length % 4 != 0)
                        return false;
                    if (base64Str.Any(c => !_base64CodeArray.Contains(c)))
                        return false;
                }
                bytes = Convert.FromBase64String(base64Str);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        /// <summary>
        ///  文件转换成Base64字符串
        /// </summary>
        /// <param name="fs">文件流</param>
        /// <returns></returns>
        public static String FileToBase64(Stream fs)
        {
            string strRet = null;
            if (fs == null) return null;
            byte[] bt = new byte[fs.Length];
            fs.Read(bt, 0, bt.Length);
            strRet = Convert.ToBase64String(bt);
            fs.Close();
            return strRet;
        }

        /// <summary>
        /// Base64字符串转换成文件
        /// <para>使用<see cref="IsBase64(string, out byte[])"/>验证是否是base64字符串</para>
        /// </summary>
        /// <param name="bytes">字节文件</param>
        /// <param name="url">保存文件的绝对路径</param>
        /// <returns></returns>
        public static void Base64ToFileAndSave(byte[] bytes, string url)
        {
            using FileStream fs = new FileStream(url, FileMode.CreateNew);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
        }
    }
}
