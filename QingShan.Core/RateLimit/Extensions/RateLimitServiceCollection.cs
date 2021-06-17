﻿using AspNetCoreRateLimit;
using QingShan.Core.ConfigurableOptions;
using QingShan.Core.RateLimit.Options;
using System;
using System.Collections.Generic;
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
        /// <param name="configure">配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddRateLimit(this IServiceCollection services, Action<IServiceCollection> configure = null)
        {
            services.AddConfigurableOptions<MineIpRateLimitOptions>();
            var build = services.BuildServiceProvider();
            

            //注入计数器和规则存储
            services.AddSingleton<IIpPolicyStore, DistributedCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, DistributedCacheRateLimitCounterStore>();
            //配置（计数器密钥生成器）
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
            return services;
        }
    }
}