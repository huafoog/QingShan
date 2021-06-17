using AspNetCoreRateLimit;
using Microsoft.Extensions.Configuration;
using QingShan.Core.ConfigurableOptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.RateLimit.Options
{
    public class MineIpRateLimitOptions: IpRateLimitOptions,IConfigurableOptions<MineIpRateLimitOptions>
    {
        /// <summary>
        /// 选项后期配置
        /// </summary>
        /// <param name="options"></param>
        /// <param name="configuration"></param>
        public void PostConfigure(MineIpRateLimitOptions options, IConfiguration configuration)
        {
        }
    }
}
