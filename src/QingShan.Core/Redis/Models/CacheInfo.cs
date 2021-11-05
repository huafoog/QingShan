using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Core.Redis.Models
{
    public class CacheInfo
    {
        /// <summary>
        /// host
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// key前缀
        /// </summary>
        public string InstanceName { get; set; }

        /// <summary>
        /// 默认 库
        /// </summary>
        public string DefaultDatabase { get; set; }
    }
}
