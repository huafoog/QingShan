using QingShan.Data.Enums;
using QingShan.DatabaseAccessor;
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
        /// 当前权限编码
        /// </summary>
        [Description("权限编码")]
        public string Code { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        [MaxLength(200)]
        public string Area { get; set; }
        /// <summary>
        /// 是否有区域
        /// </summary>
        public bool IsArea  { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        [MaxLength(200)]
        [Description("控制器")]
        public string Controller { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        [MaxLength(200)]
        [Description("操作")]
        public string Action { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [MaxLength(50)]
        [Description("模块名称")]
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [MaxLength(100)]
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
        /// 排序值
        /// </summary>
        [Description("排序值")]
        public int Sort { get; set; }
    }
}
