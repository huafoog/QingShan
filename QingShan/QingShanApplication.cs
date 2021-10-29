using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan
{
    public static class QingShanApplication
    {
        /// <summary>
        /// 配置
        /// <para>使用依赖注入时自动注入</para>
        /// </summary>
        public static IConfiguration Configuration { get; set; }
    }
}
