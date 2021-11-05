using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace QingShan.Utilities
{
    /// <summary>
    /// Http请求 使用<see cref="HttpWebRequest"/>
    /// </summary>
    public class HttpHelper
    {
        /// <summary>
        /// post请求
        /// </summary>
        /// <param name="Url">请求地址</param>
        /// <param name="postDataStr">序列化的json数据</param>
        /// <returns></returns>
        public static string HttpPost(string Url, string postDataStr)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
            //request.CookieContainer = cookie;
            System.IO.Stream myRequestStream = request.GetRequestStream();


            StreamWriter myStreamWriter = new StreamWriter(myRequestStream);
            myStreamWriter.Write(postDataStr);
            myStreamWriter.Close();

            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
             //response.Cookies = cookie.GetCookies(response.ResponseUri);
            using Stream myResponseStream = response.GetResponseStream();
            using StreamReader myStreamReader = new StreamReader(myResponseStream);
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close();

            return retString;
        }

#pragma warning disable CS1570 // XML 注释出现 XML 格式错误
        /// <summary>
        /// get请求
        /// </summary>
        /// <param name="Url">请求url</param>
        /// <param name="postDataStr">?a=1&b=2</param>
        /// <param name="headers">请求头数据</param>
        /// <returns></returns>
        public static string HttpGet(string Url, string postDataStr,Dictionary<string,string> headers)
#pragma warning restore CS1570 // XML 注释出现 XML 格式错误
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url + (postDataStr == "" ? "" : "?") + postDataStr);
            request.Method = "GET";
            request.ContentType = "text/html;charset=UTF-8";
            foreach (var item in headers)
            {
                request.Headers.Add(item.Key,item.Value);
            }
            using HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using Stream myResponseStream = response.GetResponseStream();
            using StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
            string retString = myStreamReader.ReadToEnd();
            myStreamReader.Close();
            myResponseStream.Close(); 
            return retString;
        }
    }
}
