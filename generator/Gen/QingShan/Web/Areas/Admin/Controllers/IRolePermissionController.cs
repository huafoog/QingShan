//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using System.Collections.Generic;
using System.Text;
using QingShan.Services.RolePermission;

namespace QingShan.Web.Areas.Admin.Controllers
{
    /// <summary>
	/// 角色模块
    /// </summary>
	public class RolePermissionController
	{

        private readonly IRolePermissionContract _iRolePermissionContract;

        public RolePermissionController(IRolePermissionContract iRolePermissionContract)
        {
            _iRolePermissionContract = iRolePermissionContract;
        }
	}
}
