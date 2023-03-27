using Microsoft.Extensions.Configuration;
using QingShan.Core;
using QingShan.Core.Constants;
using QingShan.Core.StaticFile;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class StaticFileServiceCollectionExtensions
    {
        /// <summary>
        /// 添加静态文件资源
        /// </summary>
        /// <param name="service">服务</param>
        /// <param name="configuration">配置</param>
        /// <returns></returns>
        public static IServiceCollection AddStaticFile(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddConfigurableOptions<StaticFileSettingsOption>(configuration);
            var staticFileSettings = configuration.GetDefultOptions<StaticFileSettingsOption>();
            if (staticFileSettings.UseDirectoryBrowser)
                service.AddDirectoryBrowser();
            return service;
        }
    }
}
