using QS.Core.Data.Enums;
using QS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QS.Core.Permission.Authorization.Functions
{
    /// <summary>
    /// 定义功能信息
    /// </summary>
    public interface IFunction : IEntity<Guid>, ILockable
    {
        /// <summary>
        /// 获取或设置 功能名称
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// 获取或设置 区域名称
        /// </summary>
        string Area { get; set; }

        /// <summary>
        /// 获取或设置 控制器名称
        /// </summary>
        string Controller { get; set; }

        /// <summary>
        /// 获取或设置 控制器的功能名称
        /// </summary>
        string Action { get; set; }

        /// <summary>
        /// 获取或设置 是否是控制器
        /// </summary>
        bool IsController { get; set; }

        /// <summary>
        /// 获取或设置 是否Ajax功能
        /// </summary>
        bool IsAjax { get; set; }

        /// <summary>
        /// 获取或设置 访问类型
        /// </summary>
        FunctionAccessType AccessType { get; set; }
    }
}
