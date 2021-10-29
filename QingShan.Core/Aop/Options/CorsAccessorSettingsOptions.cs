using AspectCore.Configuration;
using Microsoft.Extensions.Configuration;
using QingShan.Core.ConfigurableOptions;
using System;
using System.ComponentModel.DataAnnotations;

namespace QingShan.Core.Aop
{
    /// <summary>
    /// 跨域配置选项
    /// </summary>
    public sealed class AopSettingsOptions : IConfigurableOptions<AopSettingsOptions>, IAspectConfiguration
    {
        public AspectValidationHandlerCollection ValidationHandlers { get; set; }

        public InterceptorCollection Interceptors { get; set; }

        public NonAspectPredicateCollection NonAspectPredicates { get; set; }

        public bool ThrowAspectException { get; set; }

        /// <summary>
        /// 后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(AopSettingsOptions options, IConfiguration configuration)
        {
        }
    }
}