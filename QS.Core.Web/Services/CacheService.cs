using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace QS.Core.Web.Services
{
    /// <summary>
    /// 缓存服务
    /// </summary>
    public static class CacheService
    {
        /// <summary>
        /// 添加缓存服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void AddCacheService(this IServiceCollection services, IConfiguration configuration)
        {
            if (!configuration["Cache:Redis:ConnectionString"].IsNull())
            {
                //将Redis分布式缓存服务添加到服务中
                services.AddDistributedRedisCache(options =>
                {
                    //用于连接Redis的配置  Configuration.GetConnectionString("RedisConnectionString")读取配置信息的串
                    options.Configuration = configuration["Cache:Redis:ConnectionString"];// Configuration.GetConnectionString("RedisConnectionString");
                                                                                          //Redis实例名RedisDistributedCache
                    options.InstanceName = "RedisDistributedCache";
                });
            }
            else
            {
                services.AddDistributedMemoryCache();
            }
        }
    }
}
