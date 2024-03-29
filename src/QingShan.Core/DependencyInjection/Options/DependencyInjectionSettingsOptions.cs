﻿using Microsoft.Extensions.Configuration;
using QingShan.Core.ConfigurableOptions;
using QingShan.DependencyInjection;
using System;

namespace QingShan.DependencyInjection
{
    /// <summary>
    /// 依赖注入配置选项
    /// </summary>
    public sealed class DependencyInjectionSettingsOptions : IConfigurableOptions<DependencyInjectionSettingsOptions>
    {
        /// <summary>
        /// 后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(DependencyInjectionSettingsOptions options, IConfiguration configuration)
        {
        }
    }
}
