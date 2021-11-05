using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Cache
{
    public class Cache : ICache
    {
        private readonly IMemoryCache _memoryCache;

        public Cache(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }
        /// <summary>
        /// 获取string类型
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key)
        {
            return _memoryCache.Get<string>(key);
        }
        /// <summary>
        /// 获得给定键的值。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        /// <summary>
        ///  设置指定 key 的值，所有写入参数object都支持string | byte[] | 数值 | 对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="timeoutSeconds">过期(秒单位)</param>
        public async Task<bool> SetAsync(string key, object data, int timeoutSeconds = -1)
        {
            var res = _memoryCache.Set(key, data);
            return res != null;
        }
        /// <summary>
        ///  设置指定 key 的值，所有写入参数object都支持string | byte[] | 数值 | 对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="expirationTime">过期</param>
        public async Task<bool> SetAsync(string key, object data, TimeSpan expirationTime)
        {
            var res = _memoryCache.Set(key, data);
            return res != null;
        }
        /// <summary>
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public async Task<long> DeleteAsync(string key)
        {
            _memoryCache.Remove(key);
            return default;
        }
    }
}
