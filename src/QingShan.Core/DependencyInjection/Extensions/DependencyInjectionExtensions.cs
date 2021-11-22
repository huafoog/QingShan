using Microsoft.Extensions.DependencyInjection;
using QingShan.Utilities;
using System;

namespace QingShan.DependencyInjection
{
    /// <summary>
    /// 依赖注入拓展
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// 解析服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static TService GetServiceByObj<TService>(this object obj)
            where TService : class
        {
            return HttpContextUtility.GetCurrentHttpContext()?.RequestServices?.GetServiceByObj<TService>();
        }

        /// <summary>
        /// 解析服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService GetRequiredServiceByObj<TService>(this object obj)
            where TService : class
        {
            return HttpContextUtility.GetCurrentHttpContext()?.RequestServices?.GetRequiredService<TService>();
        }
    }
}
