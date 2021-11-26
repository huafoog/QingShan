using System;
using QingShan.Data;
using FreeSql.DataAnnotations;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
	/// 用户角色实体
    /// </summary>
	[Table(Name ="user_role")]
    public class UserRoleEntity : QingShan.DatabaseAccessor.EntityBase
    {

            /// <summary>
		    /// 角色Id
            /// </summary>
            public System.String RoleId { get; set; }
            
            /// <summary>
		    /// 用户Id
            /// </summary>
            public System.String UserId { get; set; }
            
    }
}
