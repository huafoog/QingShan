using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QingShan.DependencyInjection;

namespace QingShan.Core.SpecificationDocument
{
    /// <summary>
    /// 分组-排序
    /// </summary>
    [SkipScan]
    internal class GroupOrder
    {
        /// <summary>
        /// 分组名
        /// </summary>
        public string Group { get; set; }

        /// <summary>
        /// 分组排序
        /// </summary>
        public int Order { get; set; }
    }
}
