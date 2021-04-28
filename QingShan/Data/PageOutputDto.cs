using QingShan.Utilities;
using System;
using System.Collections.Generic;

namespace QingShan.Data
{
    /// <summary>
    /// 分页信息输出
    /// </summary>
    public class PageOutputDto<T>:StatusResult
    {
        /// <summary>
        /// 数据总数
        /// </summary>
        public long TotalCount { get; set; } = 0;

        public long TotalPage=> TotalCount.Division(PageSize).Ceiling().ToLong();

        public int PageNo { get; set; }

        public int PageSize { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public new IList<T> Data { get; set; }
    }
}
