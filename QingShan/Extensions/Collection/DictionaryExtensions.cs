using QingShan.DependencyInjection;
using System.Text;

namespace System.Collections.Generic
{
    /// <summary>
    /// 字典拓展类
    /// </summary>
    [SkipScan]
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 合并两个字典
        /// </summary>
        /// <param name="dic">字典</param>
        /// <param name="newDic">新字典</param>
        /// <returns></returns>
        public static Dictionary<string, string> AddOrUpdate(this Dictionary<string, string> dic, Dictionary<string, string> newDic)
        {
            foreach (var key in newDic.Keys)
            {
                if (dic.ContainsKey(key))
                {
                    dic[key] = newDic[key];
                }
                else
                {
                    dic.Add(key, newDic[key]);
                }
            }
            return dic;
        }

        /// <summary>
        /// 得到数组列表以逗号分隔的字符串
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static string GetArrayValueStr(this Dictionary<int, int> list)
        {
            StringBuilder sb = new StringBuilder();
            foreach (KeyValuePair<int, int> kvp in list)
            {
                sb.Append(kvp.Value + ",");
            }
            if (list.Count > 0)
            {
                return sb.ToString().DelLastComma();
            }
            else
            {
                return "";
            }
        }

    }
}
