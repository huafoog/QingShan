using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QingShan.Core.ConfigurableOptions;
using QingShan.DependencyInjection.Exceptions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace QingShan.DependencyInjection.Extensions
{
    /// <summary>
    /// 依赖注入拓展类
    /// </summary>
    [SkipScan]
    public static class DependencyInjectionServiceCollectionExtensions
    {
        /// <summary>
        /// 添加依赖注入接口
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDependencyInjection(this IServiceCollection services)
        {
            // 添加外部程序集配置
            services.AddAutoInjection();
            return services;
        }

        /// <summary>
        /// 添加自动扫描注入
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>服务集合</returns>
        private static IServiceCollection AddAutoInjection(this IServiceCollection services)
        {
            // 查找所有需要依赖注入的类型
            var injectTypes = AppAssembly.CanBeScanTypes
                .Where(u => typeof(IDependency).IsAssignableFrom(u) && u.IsClass && !u.IsInterface && !u.IsAbstract);

            // 执行依赖注入
            foreach (var type in injectTypes)
            {
                // 获取所有能注册的接口
                var canInjectInterfaces = type.GetInterfaces().Where(u => !typeof(IDependency).IsAssignableFrom(u));

                // 注册暂时服务
                if (typeof(ITransientDependency).IsAssignableFrom(type))
                {
                    RegisterType(services, QingShan.DependencyInjection.RegisterType.Transient, type, canInjectInterfaces.FirstOrDefault());
                }
                // 注册作用域服务
                else if (typeof(IScopeDependency).IsAssignableFrom(type))
                {
                    RegisterType(services, QingShan.DependencyInjection.RegisterType.Scoped, type, canInjectInterfaces.FirstOrDefault());
                }
                // 注册单例服务
                else if (typeof(ISingletonDependency).IsAssignableFrom(type))
                {
                    RegisterType(services, QingShan.DependencyInjection.RegisterType.Singleton, type, canInjectInterfaces.FirstOrDefault());
                }
            }
            return services;
        }

        /// <summary>
        /// 注册类型
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="registerType">注册类型</param>
        /// <param name="type">类型</param>
        /// <param name="inter">接口</param>
        private static void RegisterType(IServiceCollection services, RegisterType registerType, Type type, Type inter = null)
        {
            // 修复泛型注册类型
            var fixedType = FixedGenericType(type);
            var fixedInter = inter == null ? null : FixedGenericType(inter);
            if (registerType == QingShan.DependencyInjection.RegisterType.Transient)
            {
                RegisterTransientType(services, fixedType, fixedInter);
            }

            if (registerType == QingShan.DependencyInjection.RegisterType.Scoped)
            {
                RegisterScopeType(services, fixedType, fixedInter);
            }

            if (registerType == QingShan.DependencyInjection.RegisterType.Singleton)
            {
                RegisterSingletonType(services, fixedType, fixedInter);
            }
        }

        /// <summary>
        /// 注册瞬时接口实例类型
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="type">类型</param>
        /// <param name="inter">接口</param>
        private static void RegisterTransientType(IServiceCollection services, Type type, Type inter = null)
        {
            if (inter == null)
                services.AddTransient(type);
            else
                services.AddTransient(inter, type);

        }

        /// <summary>
        /// 注册作用域接口实例类型
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="type">类型</param>
        /// <param name="inter">接口</param>
        private static void RegisterScopeType(IServiceCollection services, Type type, Type inter = null)
        {
            if (inter == null)
                services.AddScoped(type);
            else
                services.AddScoped(inter, type);
            
        }

        /// <summary>
        /// 注册单例接口实例类型
        /// </summary>
        /// <param name="services">服务</param>
        /// <param name="type">类型</param>
        /// <param name="inter">接口</param>
        private static void RegisterSingletonType(IServiceCollection services, Type type, Type inter = null)
        {
            if (inter == null)
                services.AddSingleton(type);
            else
                services.AddSingleton(inter, type);
        }

        /// <summary>
        /// 修复泛型类型注册类型问题
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        private static Type FixedGenericType(Type type)
        {
            if (!type.IsGenericType)
            {
                return type;
            }

            return type.Assembly.GetType($"{type.Namespace}.{type.Name}", true, true);
        }

        /// <summary>
        /// 加载字符串类型
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static Type LoadStringType(string str)
        {
            var typeDefinitions = str.Split(";");
            var assembly = AppAssembly.Assemblies.First(u => u.GetName().Name == typeDefinitions[0]);
            return assembly.GetType(typeDefinitions[1], true, true);
        }
    }
}
