using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QS.Core
{
    /// <summary>
    /// 代理拦截依赖接口
    /// </summary>
    public interface IDispatchProxy
    {
        /// <summary>
        /// 实例
        /// </summary>
        object Target { get; set; }

        /// <summary>
        /// 服务提供器
        /// </summary>
        IServiceProvider Services { get; set; }
    }
}
