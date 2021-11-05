using QingShan.CodeGeneratorWeb.CodeGenerator;
using QingShan.Core.CodeGenerator;
using QingShan.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    [SkipScan]
    public static class CodeGeneratorServiceCollectionExtensions
    {

        /// <summary>
        /// 添加策略授权服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">自定义配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddCodeGenerator(this IServiceCollection services, Action<IServiceCollection> configure = null)
        {
            services.AddSingleton<ICodeGenService, CodeGenService>();
            return services;
        }
    }
}
