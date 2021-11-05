//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using System.Collections.Generic;
using System.Text;
using QingShan.Services.Permission;

namespace QingShan.Web.Areas.Admin.Controllers
{
    /// <summary>
	/// 权限
    /// </summary>
	public class PermissionController
	{

        private readonly IPermissionContract _iPermissionContract;

        public PermissionController(IPermissionContract iPermissionContract)
        {
            _iPermissionContract = iPermissionContract;
        }
	}
}
