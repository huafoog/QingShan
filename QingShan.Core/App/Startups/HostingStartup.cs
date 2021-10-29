using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using QingShan.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
[assembly: HostingStartup(typeof(QingShan.Core.HostingStartup))]
namespace QingShan.Core
{
    /// <summary>
    /// 配置程序启动时自动注入
    /// </summary>
    [SkipScan]
    public sealed class HostingStartup : IHostingStartup
    {
        /// <summary>
        /// 配置应用启动
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(IWebHostBuilder builder)
        {
            // 自动注入 AddApp() 服务
            builder.ConfigureServices(services =>
            {
            });
        }
    }
}
