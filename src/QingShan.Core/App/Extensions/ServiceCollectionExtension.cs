using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// 获取配置
        /// </summary>
        public static IConfiguration GetConfiguration(this IServiceCollection services)
        {

            var webHostBuilderContext = services.GetSingletonInstanceOrNull<WebHostBuilderContext>();
            if (webHostBuilderContext?.Configuration != null)
            {
                return webHostBuilderContext.Configuration as IConfigurationRoot;
            }

            return services.GetSingletonInstance<IConfiguration>();
        }

        /// <summary>
        /// 获取一个注册实例（可为空）
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {
            ServiceDescriptor data = services
                .FirstOrDefault(d => d.ServiceType == typeof(T));
            return (T)data?.ImplementationInstance;
        }

        /// <summary>
        /// 获取一个注册实例
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public static T GetSingletonInstance<T>(this IServiceCollection services)
            where T : class
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            if (service == null)
            {
                throw new InvalidOperationException("Could not find singleton service: " + typeof(T).AssemblyQualifiedName);
            }
            
            return service;
        }

        /// <summary>
        ///获取选项
        /// </summary>
        public static Options GetOptions<Options>(this ServiceProvider serviceProvider)
            where Options : class
        {
            var option = serviceProvider.GetService<IOptionsMonitor<Options>>()?.CurrentValue;
            return option??default;
        }
    }
}
