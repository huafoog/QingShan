using System;

namespace QingShan.Data
{
    /// <summary>
    /// 分页输入参数
    /// </summary>
    public class PageInputDto : PageBaseInputDto
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string Search { get; set; }
    }
    public class PageBaseInputDto
    {
        /// <summary>
        /// 当前页标
        /// </summary>
        public int PageNo { get; set; } = 1;

        /// <summary>
        /// 每页大小
        /// </summary>
        public int? PageSize { set; get; } = 10;
    }
}
