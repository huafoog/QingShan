using QingShan.DataLayer.Enums;
using QingShan.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Services.Permission.Dto
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class PermissionListOutputDto : TreeNodeDto<PermissionListOutputDto>
    {
        /// <summary>
        /// 菜单编码 格式:system
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 自动生成 权限代码 格式:system.menu.add
        /// </summary>
        public string PermissionCode { get; set; }
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
        /// 排序值
        /// </summary>
        public int Sort { get; set; }
        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }
        public string Title => Name;
        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType PermissionType { get; set; }
    }
}
