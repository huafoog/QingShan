using FreeSql.DataAnnotations;
using QingShan.DatabaseAccessor;
using System;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
    /// 角色模块
    /// </summary>
    [Table(Name = "role_permission")]
    public class RolePermissionEntity : EntityBase
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 权限id
        /// </summary>
        public string PermissionId { get; set; }
    }
}
