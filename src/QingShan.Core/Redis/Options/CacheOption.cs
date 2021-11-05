using Microsoft.Extensions.Options;
using QingShan.Core.ConfigurableOptions;
using QingShan.Core.Redis.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.Redis.Options
{

    /// <summary>
    /// 
    /// </summary>
    [OptionsSettings("Cache")]
    public sealed class CacheOption: IConfigurableOptions
    {
        /// <summary>
        /// 缓存方式 Redis
        /// </summary>
        public string CacheWay { get; set; }
        public CacheInfo Redis { get; set; }
    }
}
