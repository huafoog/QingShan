using QS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public class RolePermissionEntity:EntityBase<int>
    {
        /// <summary>
        /// 角色Id
        /// </summary>
		public int RoleId { get; set; }

        /// <summary>
        /// 权限Id
        /// </summary>
		public int PermissionId { get; set; }
    }
}
