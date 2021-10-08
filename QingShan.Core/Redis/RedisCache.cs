using CSRedis;
using Microsoft.Extensions.Options;
using QingShan.Cache;
using QingShan.Core.Redis.Options;
using System;
using System.Threading.Tasks;

namespace QingShan.Core.Redis
{
    public class RedisCache : ICache
    {
        private readonly CSRedisClient _redisClient;
        private readonly IOptions<CacheOption> _cacheOptions;

        public RedisCache(CSRedisClient redisClient, IOptions<CacheOption> cacheOptions)
        {
            _redisClient = redisClient;
            _cacheOptions = cacheOptions;
        }

        /// <summary>
        /// 获取string类型
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public async Task<string> GetAsync(string key)
        {
            return await _redisClient.GetAsync<string>(key);
        }
        /// <summary>
        /// 获得给定键的值。
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public async Task<T> GetAsync<T>(string key)
        {
            return await _redisClient.GetAsync<T>(key);
        }

        /// <summary>
        ///  设置指定 key 的值，所有写入参数object都支持string | byte[] | 数值 | 对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="timeoutSeconds">过期(秒单位)</param>
        public async Task<bool> SetAsync(string key, object data, int timeoutSeconds = -1)
        {
            return await _redisClient.SetAsync(key, data, timeoutSeconds);
        }
        /// <summary>
        ///  设置指定 key 的值，所有写入参数object都支持string | byte[] | 数值 | 对象
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        /// <param name="expirationTime">过期</param>
        public async Task<bool> SetAsync(string key, object data, TimeSpan expirationTime)
        {
            return await _redisClient.SetAsync(key, data, expirationTime);
        }
        /// <summary>
        /// 用于在 key 存在时删除 key
        /// </summary>
        /// <param name="key">缓存键</param>
        /// <returns></returns>
        public async Task<long> DeleteAsync(string key)
        {
            return await _redisClient.DelAsync(key);
        }
    }
}
