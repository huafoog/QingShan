using QS.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 权限实体
    /// </summary>
    public class PermissionEntity:EntityBase<int>
    {
        /// <summary>
        /// 方法id
        /// </summary>
        public Guid FunctionId { get; set; }

        /// <summary>
        /// 父级节点
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        [Column(TypeName = "nvarchar(500)")]
        public string Code { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType Type { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; } = 0;
    }

    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PermissionType
    {
        /// <summary>
        /// 分组
        /// </summary>
        Group = 1,
        /// <summary>
        /// 菜单
        /// </summary>
        Menu = 2,
        /// <summary>
        /// 权限点
        /// </summary>
        Dot = 3
    }
}
