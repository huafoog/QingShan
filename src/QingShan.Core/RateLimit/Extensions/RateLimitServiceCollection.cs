using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;
using QingShan.Core.ConfigurableOptions;
using QingShan.Core.RateLimit;
using QingShan.Core.RateLimit.Options;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class RateLimitServiceCollection
    {
        /// <summary>
        /// 添加限流
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">配置委托</param>
        /// <param name="configuration">配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddRateLimit(this IServiceCollection services,IConfiguration configuration, Action<IServiceCollection> configure = null)
        {
            services.AddConfigurableOptions<MineIpRateLimitOptions>(configuration);
            services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
            //注入计数器和规则存储
            services.AddSingleton<IIpPolicyStore, RedisIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, RedisCounterStore>();
            //配置（计数器密钥生成器）
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            return services;
        }
    }
}
