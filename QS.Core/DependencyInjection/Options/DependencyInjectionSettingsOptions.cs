using Microsoft.Extensions.Configuration;
using QS.Core.ConfigurableOptions;
using QS.Core.DependencyInjection.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Core.DependencyInjection
{
    /// <summary>
    /// 依赖注入配置选项
    /// </summary>
    public sealed class DependencyInjectionSettingsOptions : IConfigurableOptions<DependencyInjectionSettingsOptions>
    {
        /// <summary>
        /// 外部注册定义
        /// </summary>
        public ExternalService[] Definitions { get; set; }

        /// <summary>
        /// 后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(DependencyInjectionSettingsOptions options, IConfiguration configuration)
        {
            options.Definitions ??= Array.Empty<ExternalService>();
        }
    }
}
