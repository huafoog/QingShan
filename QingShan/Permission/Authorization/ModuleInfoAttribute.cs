using QingShan.Data.Enums;
using System;
using System.ComponentModel;

namespace QingShan.Permission.Authorization
{
    /// <summary>
    /// 模型描述
    /// </summary>
    public class ModuleInfoAttribute : Attribute
    {

        /// <summary>
        /// 当前URL路径 当前模块为菜单时需要填写url
        /// <para>若不填写将选择以下方式</para>
        /// <para>方式1：父级Code+当前code</para>
        /// <para>方式2：类名+当前code</para>
        /// <para>方式3：当前code</para>
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// 当前模块
        /// <para>该字段在菜单时使用</para>
        /// </summary>
        public ModuleEnum Module { get; set; } = ModuleEnum.Null;

        /// <summary>
        /// 上级代码
        /// <para>模块类型为菜单时自动生成上级信息</para>
        /// <para>模块类型为操作时若未填写上级代码则区当前方法的code</para>
        /// <para>填写方法名称</para>
        /// </summary>
        public string P_Code { get; set; }

        /// <summary>
        /// 上级名称
        /// </summary>
        public string P_Name { get; set; }

        /// <summary>
        /// 上级跳转地址
        /// </summary>
        public string P_Url { get; set; }

        /// <summary>
        /// 方法代码
        /// <para>若不填则取当前方法名称</para>
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// <para>请使用<see cref="DescriptionAttribute"/>特性 为空去功能名称</para>
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序字段
        /// </summary>
        public int Sort { get; set; }
    }
}
