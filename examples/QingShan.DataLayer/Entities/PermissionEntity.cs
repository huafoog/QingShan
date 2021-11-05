using QingShan.Data.Enums;
using QingShan.DatabaseAccessor;
using QingShan.DataLayer.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
    /// 权限
    /// </summary>
    public class PermissionEntity : EntityBase
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        [Description("菜单编码")]
        [Required]
        public string Code { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [MaxLength(50)]
        [Description("模块名称")]
        [Required]
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [MaxLength(200)]
        [Description("路径")]
        public string Path { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [MaxLength(500)]
        [Description("图标")]
        public string Icon { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        [MaxLength(100)]
        [Description("备注信息")]
        public string Remark { get; set; }
        /// <summary>
        /// 组件
        /// </summary>
        [MaxLength(200)]
        [Description("组件地址")]
        public string Component { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        [Description("排序值")]
        public int Sort { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        [Description("权限类型")]
        [FreeSql.DataAnnotations.Column(MapType = typeof(int))]
        public PermissionType PermissionType { get; set; }

        /// <summary>
        /// 自动生成 权限代码 格式:system.menu.add
        /// </summary>
        [MaxLength(200)]
        [Required]
        [Description("权限代码")]
        public string PermissionCode { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        [MaxLength(36)]
        [Description("父级id")]
        public string ParentId { get; set; }
    }
}
