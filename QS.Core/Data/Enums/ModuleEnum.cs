using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QS.Core.Data.Enums
{
    /// <summary>
    /// 模块
    /// </summary>
    public enum ModuleEnum
    {
        /// <summary>
        /// 系统管理
        /// </summary>
        [Description("系统管理")]
        System = 0,

        /// <summary>
        /// 默认值 空
        /// </summary>
        [Description("空")]
        Null = 100
    }
}
