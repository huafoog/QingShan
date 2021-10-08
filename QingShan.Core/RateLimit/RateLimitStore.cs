using AspNetCoreRateLimit;
using Newtonsoft.Json;
using QingShan.Cache;
using QingShan.Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace QingShan.Core.RateLimit
{
    /// <summary>
    /// 重写Redis
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RateLimitStore<T>: IRateLimitStore<T>
    {
        private readonly ICache _cache;

        public RateLimitStore(ICache cache)
        {
            _cache = cache;
        }

        public Task SetAsync(string id, T entry, TimeSpan? expirationTime = null, CancellationToken cancellationToken = default)
        {
            return _cache.SetAsync(RateLimitConst.UserBehaviorCache.ToFormat(id), entry, expirationTime?? TimeSpan.FromDays(1));
        }

        public async Task<bool> ExistsAsync(string id, CancellationToken cancellationToken = default)
        {
            var stored = await _cache.GetAsync(RateLimitConst.UserBehaviorCache.ToFormat(id));

            return !string.IsNullOrEmpty(stored);
        }

        public async Task<T> GetAsync(string id, CancellationToken cancellationToken = default)
        {
            var stored = await _cache.GetAsync(RateLimitConst.UserBehaviorCache.ToFormat(id));

            if (!string.IsNullOrEmpty(stored))
            {
                return JsonConvert.DeserializeObject<T>(stored);
            }

            return default;
        }

        public Task RemoveAsync(string id, CancellationToken cancellationToken = default)
        {
            return _cache.DeleteAsync(RateLimitConst.UserBehaviorCache.ToFormat(id));
        }
    }
}
