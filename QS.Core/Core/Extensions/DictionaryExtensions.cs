using QS.Core.DependencyInjection;

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

    }
}
