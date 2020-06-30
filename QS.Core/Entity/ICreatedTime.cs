using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QS.Core.Entity
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
