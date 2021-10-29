using AspectCore.Configuration;
using AspectCore.Extensions.DependencyInjection;
using QingShan.Core.Aop;
using QingShan.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 跨域访问服务拓展类
    /// </summary>
    [SkipScan]
    public static class AopServiceCollectionExtensions
    {
        /// <summary>
        /// 配置跨域
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure"></param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddAop(this IServiceCollection services, Action<IAspectConfiguration> configure = null)
        {
            services.ConfigureDynamicProxy(config=> {
                config.Interceptors.AddTyped<TestOneAttribute>();//这个是需要全局拦截的拦截器
                configure?.Invoke(config);
            });

            return services;
        }
    }
}