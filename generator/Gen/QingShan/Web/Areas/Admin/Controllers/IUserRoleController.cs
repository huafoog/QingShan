//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using System.Collections.Generic;
using System.Text;
using QingShan.Services.UserRole;

namespace QingShan.Web.Areas.Admin.Controllers
{
    /// <summary>
	/// 用户角色实体
    /// </summary>
	public class UserRoleController
	{

        private readonly IUserRoleContract _iUserRoleContract;

        public UserRoleController(IUserRoleContract iUserRoleContract)
        {
            _iUserRoleContract = iUserRoleContract;
        }
	}
}
