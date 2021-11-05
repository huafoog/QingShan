using FreeSql.DataAnnotations;
using QingShan.Data.Enums;
using QingShan.DatabaseAccessor;
using QingShan.DataLayer.Enums;
using System;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
    /// 权限
    /// </summary>
    [Table(Name = "permission")]
    public class PermissionEntity : EntityBase
    {
        /// <summary>
        /// 菜单编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        [FreeSql.DataAnnotations.Column(MapType = typeof(int))]
        public PermissionType PermissionType { get; set; }

        /// <summary>
        /// 自动生成 权限代码 格式:system.menu.add
        /// </summary>
        public string PermissionCode { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public string ParentId { get; set; }
    }
}
