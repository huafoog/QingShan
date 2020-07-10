using System;
using System.Collections.Generic;
using System.Text;

namespace QS.Core.Data
{
    /// <summary>
    /// 分页信息输出
    /// </summary>
    public class PageOutputDto<T>
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public long Total { get; set; } = 0;

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public IList<T> Data { get; set; }
    }
}
