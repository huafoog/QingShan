using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace QS.Core.Data.Enums
{
    /// <summary>
    /// 模块
    /// <para>该枚举值包含 Name,Code,Sort</para>
    /// <para>其中 code为 枚举<see cref="String"/>值</para>
    /// <para>Name为枚举的<see cref="DescriptionAttribute"/></para>
    /// <para>Sort为枚举<see cref="Int32"/>值</para>
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
