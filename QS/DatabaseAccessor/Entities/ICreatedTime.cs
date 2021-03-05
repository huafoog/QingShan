using System;
using System.ComponentModel;

namespace QS.DatabaseAccessor
{
    /// <summary>
    /// 创建时间
    /// </summary>
    public interface ICreatedTime
    {
        /// <summary>
        /// 获取或设置 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        DateTime CreateTime { get; set; }
    }
}
