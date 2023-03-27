using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using QingShan.ConfigurableOptions;
using QingShan.Core;
using QingShan.Core.ConfigurableOptions;
using QingShan.DependencyInjection;
using QingShan.Exceptions;
using System;
using System.Linq;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 可变选项服务拓展类
    /// </summary>
    [SkipScan]
    public static class ConfigurableOptionsServiceCollectionExtensions
    {

        /// <summary>
        /// 添加选项配置
        /// </summary>
        /// <typeparam name="TOptions">选项类型</typeparam>
        /// <param name="services">服务集合</param>
        /// <param name="configureBinder"></param>
        /// <param name="configurationRoot">配置</param>
        /// <param name="key"></param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddConfigurableOptions<TOptions>(this IServiceCollection services,IConfiguration configurationRoot, Action<BinderOptions> configureBinder, string key = null)
            where TOptions : class, IConfigurableOptions
        {
            var optionsType = typeof(TOptions);
            var optionsSettings = optionsType.GetCustomAttribute<OptionsSettingsAttribute>(false);

            // 获取键名
            var jsonKey = JsonKey.GetOptionsJsonKey(optionsSettings, optionsType);
            var optionsConfiguration = configurationRoot.GetSection(key ?? jsonKey);
            if (configureBinder == null)
            {
                services.Configure<TOptions>(optionsConfiguration, configureBinder);
            }
            else
            {
                services.Configure<TOptions>(optionsConfiguration);
            }

            // 配置复杂验证后后期配置
            var validateInterface = optionsType.GetInterfaces()
                .FirstOrDefault(u => u.IsGenericType && typeof(IConfigurableOptions).IsAssignableFrom(u.GetGenericTypeDefinition()));
            if (validateInterface != null)
            {
                var genericArguments = validateInterface.GenericTypeArguments;

                // 配置复杂验证
                if (genericArguments.Length > 1)
                {
                    services.TryAddEnumerable(ServiceDescriptor.Singleton(typeof(IValidateOptions<TOptions>), genericArguments.Last()));
                }

                // 配置后期配置
                var postConfigureMethod = optionsType.GetMethod(nameof(IConfigurableOptions<TOptions>.PostConfigure));
                if (postConfigureMethod != null)
                {
                    if (optionsSettings?.PostConfigureAll != true)
                    {
                        services.PostConfigure<TOptions>(options => postConfigureMethod.Invoke(options, new object[] { options, optionsConfiguration }));
                    }
                    else
                    {
                        services.PostConfigureAll<TOptions>(options => postConfigureMethod.Invoke(options, new object[] { options, optionsConfiguration }));
                    }
                }
            }

            return services;
        }

        public static IServiceCollection AddConfigurableOptions<TOptions>(this IServiceCollection services, IConfiguration configurationRoot, string key = null)
        where TOptions : class, IConfigurableOptions
        {
            return AddConfigurableOptions<TOptions>(services,configurationRoot, null, key);
        }

        /// <summary>
        /// 注入配置文件
        /// <para></para>
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuation"></param>
        /// <returns></returns>
        [Obsolete("移除APP.Configurable")]
        public static IServiceCollection AddConfigurable(this IServiceCollection services, IConfiguration configuation)
        {
            return services;
        }
    }
}
