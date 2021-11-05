using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QingShan.Cache
{
    /// <summary>
    /// 缓存
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<string> GetAsync(string key);
        /// <summary>
        /// 获取
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task<T> GetAsync<T>(string key);
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="expiresSec">过期（秒）</param>
        Task<bool> SetAsync(string key, object t, int expiresSec = -1);
        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="expirationTime">过期</param>
        Task<bool> SetAsync(string key, object t, TimeSpan expirationTime);

        /// <summary>
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        Task<long> DeleteAsync(string key);
    }
}
