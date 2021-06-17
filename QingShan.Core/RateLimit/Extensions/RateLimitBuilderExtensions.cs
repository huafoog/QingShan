using QingShan.DependencyInjection;
using QingShan.Core.SpecificationDocument;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
using QingShan.Core.RateLimit.Options;
using AspNetCoreRateLimit;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 使用限流 --- 占时无用
    /// </summary>
    [SkipScan]
    public static class RateLimitBuilderExtensions
    {
        /// <summary>
        /// 使用限流
        /// </summary>
        /// <param name="app"></param>
        /// <param name="routePrefix"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseRateLimit(this IApplicationBuilder app, string routePrefix = default)
        {
            app.UseMiddleware<IpRateLimitMiddleware>();
            return app;
        }
    }
}