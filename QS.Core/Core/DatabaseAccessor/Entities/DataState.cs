using System.ComponentModel;

namespace QS.Core.DatabaseAccessor
{
    /// <summary>
    /// 数据状态
    /// </summary>
    public enum DataState
    {
        /// <summary>
        /// 正常
        /// </summary>
        [Description("正常")]
        Normal = 0,


        /// <summary>
        /// 异常
        /// </summary>
        [Description("异常")]
        Abnormal = 1,

        /// <summary>
        /// 删除
        /// </summary>
        [Description("删除")]
        Delete = 2

    }
}
