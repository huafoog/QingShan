using Panda.DynamicWebApi;
using QingShan.Core;
using QingShan.Core.ConfigurableOptions;
using QingShan.DependencyInjection;
using QingShan.DependencyInjection.Extensions;
using QingShan.Core.Options;
using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 应用服务集合拓展类
    /// </summary>
    [SkipScan]
    public static class AppServiceCollectionExtensions
    {
        /// <summary>
        /// 服务注入基础配置
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="Configuration">配置文件</param>
        /// <returns>IMvcBuilder</returns>
        public static IServiceCollection AddInject(this IServiceCollection services, IConfiguration Configuration)
        {
            QingShan.QingShanApplication.Configuration = Configuration;
            services
                .AddSpecificationDocuments()
                .AddCache()
                .AddDatabaseAccessor();
            return services;
        }

        /// <summary>
        /// 添加应用配置
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">配置</param>
        /// <param name="configuration">服务配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddApp(this IServiceCollection services, IConfiguration configuration, Action<IServiceCollection> configure = null)
        {
            // 添加 HttContext 访问器
            services.AddHttpContextAccessor();

            // 注册全局依赖注入
            services.AddDependencyInjection();

            services.AddInject(configuration);

            // 自定义服务
            configure?.Invoke(services);

            return services;
        }
    }
}