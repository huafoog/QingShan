using CSRedis;
using Microsoft.Extensions.Caching.Distributed;
using QingShan.Cache;
using QingShan.Core.ConfigurableOptions;
using QingShan.Core.Redis;
using QingShan.Core.Redis.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection  
{
    public static class RedisCollectionExtension
    {
        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddCache(this IServiceCollection services)
        {
            services.AddConfigurableOptions<CacheOption>();
            var cacheOption = services.GetOptions<CacheOption>();
            if (cacheOption.CacheWay.IsNull())
            {
                throw new CacheErrorException("错误的缓存方式");
            }
            if (cacheOption.CacheWay.ToUpper() == "Redis".ToUpper())
            {
                services.AddSingleton(new CSRedisClient($"{cacheOption.Redis.Host},password={cacheOption.Redis.Password},defaultDatabase={cacheOption.Redis.DefaultDatabase}"));
                services.AddSingleton<ICache, RedisCache>();
            }
            return services;
        }
    }
}
