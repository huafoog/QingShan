using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
namespace QingShan.Utilities
{
    /// <summary>
    /// DES����/�����ࡣ
    /// </summary>
    public class DESEncrypt
    {
        //Ĭ����Կ���� 
        private static byte[] Keys = { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F };
        /// <summary> 
        /// DES�����ַ��� 
        /// </summary> 
        /// <param name="encryptString">�����ܵ��ַ���</param> 
        /// <param name="encryptKey">������Կ,Ҫ��Ϊ16λ</param> 
        /// <returns>���ܳɹ����ؼ��ܺ���ַ�����ʧ�ܷ���Դ��</returns> 
        public static string Encrypt(string encryptString, string encryptKey = "Key123Ace#321Key")
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(encryptKey.Substring(0, 16));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Encoding.UTF8.GetBytes(encryptString);
                var DCSP = Aes.Create();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateEncryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Convert.ToBase64String(mStream.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message + encryptString;
            }

        }
        /// <summary> 
        /// DES�����ַ��� 
        /// </summary> 
        /// <param name="decryptString">�����ܵ��ַ���</param> 
        /// <param name="decryptKey">������Կ,Ҫ��Ϊ16λ,�ͼ�����Կ��ͬ</param> 
        /// <returns>���ܳɹ����ؽ��ܺ���ַ�����ʧ�ܷ�Դ��</returns> 
        public static string Decrypt(string decryptString, string decryptKey = "Key123Ace#321Key")
        {
            try
            {
                byte[] rgbKey = Encoding.UTF8.GetBytes(decryptKey.Substring(0, 16));
                byte[] rgbIV = Keys;
                byte[] inputByteArray = Convert.FromBase64String(decryptString);
                var DCSP = Aes.Create();
                MemoryStream mStream = new MemoryStream();
                CryptoStream cStream = new CryptoStream(mStream, DCSP.CreateDecryptor(rgbKey, rgbIV), CryptoStreamMode.Write);
                Byte[] inputByteArrays = new byte[inputByteArray.Length];
                cStream.Write(inputByteArray, 0, inputByteArray.Length);
                cStream.FlushFinalBlock();
                return Encoding.UTF8.GetString(mStream.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message + decryptString;
            }

        }
    }
}
