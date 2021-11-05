using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Services.Permission.Dto
{
    public class RoleTreeOutputDto
    {
        /// <summary>
        /// 默认选中的树节点
        /// </summary>
        public List<string> DefaultSelectedKeys { get; set; }

        /// <summary>
        /// 权限数据
        /// </summary>
        public List<PermissionListOutputDto> Data { get; set; }

        /// <summary>
        /// 权限原始数据集合
        /// </summary>
        public List<PermissionListOutputDto> List { get; set; }
    }
}
