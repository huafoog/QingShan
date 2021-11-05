//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using QingShan.Data;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
	/// 角色模块
    /// </summary>
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
