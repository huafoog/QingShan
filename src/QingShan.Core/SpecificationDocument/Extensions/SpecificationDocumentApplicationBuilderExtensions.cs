using QingShan.DependencyInjection;
using QingShan.Core.SpecificationDocument;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyInjection;
namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 规范化文档中间件拓展
    /// </summary>
    [SkipScan]
    public static class SpecificationDocumentsBuilderExtensions
    {
        /// <summary>
        /// 添加规范化文档中间件
        /// </summary>
        /// <param name="app"></param>
        /// <param name="routePrefix"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseSpecificationDocuments(this IApplicationBuilder app, string routePrefix = default)
        {
            var config = app.ApplicationServices.GetService<IOptions<SpecificationDocumentSettingsOptions>>().Value;
            if (config.IsView == false)
            {
                return app;
            }
            SpecificationDocumentBuilder.Init(config);
            // 配置 Swagger 全局参数
            app.UseSwagger(options =>
            {
                SpecificationDocumentBuilder.Build(options);
            });
            // 配置 Swagger UI 参数
            app.UseSwaggerUI(c => SpecificationDocumentBuilder.BuildUI(c, routePrefix));
            return app;
        }
    }
}