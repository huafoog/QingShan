using QingShan.Core.ConfigurableOptions;
using QingShan.DependencyInjection;
using QingShan.Core.SpecificationDocument;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Reflection;
using System.IO;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.Extensions.Configuration;
using QingShan.Core;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 规范化接口服务拓展类
    /// </summary>
    [SkipScan]
    public static class SpecificationDocumentServiceCollectionExtensions
    {
        /// <summary>
        /// 添加规范化文档服务
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddSpecificationDocuments(this IServiceCollection services)
        {
            services.AddConfigurableOptions<SpecificationDocumentSettingsOptions>();
            var config = App.GetDefultOptions<SpecificationDocumentSettingsOptions>();
            if (config.IsView == false)
            {
                return services;
            }
            services.AddSwaggerGen(c =>
            {
                SpecificationDocumentBuilder.BuildGen(c);
            });
            return services;
        }
    }
}