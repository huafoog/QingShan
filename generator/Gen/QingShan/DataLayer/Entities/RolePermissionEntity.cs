using System;
using QingShan.Data;
using FreeSql.DataAnnotations;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
	/// 角色模块
    /// </summary>
	[Table(Name ="role_permission")]
    public class RolePermissionEntity : QingShan.DatabaseAccessor.EntityBase
    {

            /// <summary>
		    /// 权限id
            /// </summary>
            public System.String PermissionId { get; set; }
            
            /// <summary>
		    /// 角色Id
            /// </summary>
            public System.String RoleId { get; set; }
            
    }
}
