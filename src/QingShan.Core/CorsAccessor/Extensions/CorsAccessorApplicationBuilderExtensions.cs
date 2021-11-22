using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using QingShan.Core;
using QingShan.Core.CorsAccessor;
using QingShan.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 跨域中间件拓展
    /// </summary>
    [SkipScan]
    public static class CorsAccessorApplicationBuilderExtensions
    {
        /// <summary>
        /// 添加跨域中间件
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCorsAccessor(this IApplicationBuilder app)
        {
            var corsAccessorSettings = App.GetDefultOptions<CorsAccessorSettingsOptions>();

            // 配置跨域中间件
            app.UseCors(corsAccessorSettings.PolicyName);

            // 添加压缩缓存
            app.UseResponseCaching();

            return app;
        }
    }
}