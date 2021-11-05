//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using QingShan.Data;

namespace Demo.Data.Entities
{
    /// <summary>
	/// 用户角色实体
    /// </summary>
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
