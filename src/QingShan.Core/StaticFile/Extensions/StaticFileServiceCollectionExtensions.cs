using QingShan.Core;
using QingShan.Core.StaticFile;

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
            var staticFileSettings  = App.GetDefultOptions<StaticFileSettingsOption>();
            if (staticFileSettings.UseDirectoryBrowser)
                service.AddDirectoryBrowser();
            return service;
        }
    }
}
