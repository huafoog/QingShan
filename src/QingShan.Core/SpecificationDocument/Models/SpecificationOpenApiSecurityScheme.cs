using Microsoft.OpenApi.Models;
using QingShan.DependencyInjection;

namespace QingShan.Core.SpecificationDocument
{
    /// <summary>
    /// 规范化稳定安全配置
    /// </summary>
    [SkipScan]
    public sealed class SpecificationOpenApiSecurityScheme : OpenApiSecurityScheme
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public SpecificationOpenApiSecurityScheme()
        {
        }

        /// <summary>
        /// 唯一Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 安全需求
        /// </summary>
        public SpecificationOpenApiSecurityRequirementItem Requirement { get; set; }
    }
}