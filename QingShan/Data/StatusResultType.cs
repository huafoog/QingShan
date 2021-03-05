using System.ComponentModel;

namespace QingShan.Data
{
    /// <summary>
    /// 表示业务操作结果的枚举
    /// </summary>
    public enum StatusResultType
    {
        /// <summary>
        ///   操作成功
        /// </summary>
        [Description("操作成功。")]
        Success,

        /// <summary>
        ///   操作引发错误
        /// </summary>
        [Description("操作引发错误。")]
        Error,
        /// <summary>
        /// 警告
        /// </summary>
        Warnning
    }
}
