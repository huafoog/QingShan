using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Services.Permission.Dto
{
    /// <summary>
    /// 设置角色权限
    /// </summary>
    public class SetRolePermissionInputDto
    {
        /// <summary>
        /// 角色id
        /// </summary>
        [Required(ErrorMessage ="未获取到角色信息")]public string RoleId { get; set; }

        /// <summary>
        /// 权限Id集合
        /// </summary>
        [Required(ErrorMessage = "未获取到权限信息")] public List<string> PermissionIds { get; set; }
    }
}
