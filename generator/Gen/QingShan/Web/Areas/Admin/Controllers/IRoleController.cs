//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using System.Collections.Generic;
using System.Text;
using QingShan.Services.Role;

namespace QingShan.Web.Areas.Admin.Controllers
{
    /// <summary>
	/// 角色模型
    /// </summary>
	public class RoleController
	{

        private readonly IRoleContract _iRoleContract;

        public RoleController(IRoleContract iRoleContract)
        {
            _iRoleContract = iRoleContract;
        }
	}
}
