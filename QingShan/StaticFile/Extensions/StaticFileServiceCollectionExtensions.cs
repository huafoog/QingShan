using Microsoft.Extensions.Options;
using QingShan.Core.ConfigurableOptions;
using QingShan.StaticFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StaticFileServiceCollectionExtensions
    {
        /// <summary>
        /// 添加静态文件资源
        /// </summary>
        /// <param name="service"></param>
        /// <returns></returns>
        public static IServiceCollection AddStaticFile(this IServiceCollection service)
        {
            service.AddConfigurableOptions<StaticFileSettingsOption>();
            var provider = service.BuildServiceProvider();
            var staticFileSettings = provider.GetService<IOptions<StaticFileSettingsOption>>().Value;
            if (staticFileSettings.UseDirectoryBrowser)
                service.AddDirectoryBrowser();
            return service;
        }
    }
}
