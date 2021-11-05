using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using QingShan.CodeGeneratorWeb.CodeGenerator;
using QingShan.CodeGeneratorWeb.CodeGenerator.Builders;
using QingShan.DependencyInjection;

namespace Microsoft.AspNetCore.Builder
{
    /// <summary>
    /// 跨域代码自动生成
    /// </summary>
    [SkipScan]
    public static class CodeGeneratorApplicationBuilderExtensions
    {
        /// <summary>
        /// 添加代码生成
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder UseCodeGenerator(this IApplicationBuilder app)
        {
            CodeGeneratorCode.Init();
            return app;
        }
    }
}