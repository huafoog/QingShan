using System.ComponentModel;

namespace QS.Core.Data.Enums
{
    /// <summary>
    /// 模块节点
    /// </summary>
    public enum ModuleLevel
    {
        /// <summary>
        /// 模块
        /// </summary>
        [Description("模块")]
        Module = 1,
        /// <summary>
        /// 菜单
        /// </summary>
        [Description("菜单")]
        Menu = 2,
        /// <summary>
        /// 方法
        /// </summary>
        [Description("方法")]
        Function = 3
    }
}
