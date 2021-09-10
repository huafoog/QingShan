using QingShan.Attributes;
using QingShan.Data.Enums;
using QingShan.DataLayer.Entities;
using QingShan.DataLayer.Enums;
using System;
using System.ComponentModel.DataAnnotations;

namespace QingShan.Services.Permission.Dto
{
    public class UpdatePermissionInputDto: PermissionInputDto
    {
        public string Id { get; set; }
    }

    public class PermissionInputDto
    {
        /// <summary>
        /// 菜单编码 格式:system
        /// </summary>
        [Required(ErrorMessage = "请输入菜单编码")]
        public string Code { get; set; }

        /// <summary>
        /// 自动生成 权限代码 格式:system.menu.add
        /// </summary>
        [Required(ErrorMessage = "请输入权限代码")]
        public string PermissionCode { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [Required(ErrorMessage = "请输入模块名称")] public string Name { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        [Required(ErrorMessage = "请输入路径")] public string Path { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
        /// <summary>
        /// 备注信息
        /// </summary>
        public string Remark { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public string ParentId { get; set; }
        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType PermissionType { get; set; }
    }
}
