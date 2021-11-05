//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using System.Collections.Generic;
using System.Text;
using QingShan.Services.User;

namespace QingShan.Web.Areas.Admin.Controllers
{
    /// <summary>
	/// 用户表
    /// </summary>
	public class UserController
	{

        private readonly IUserContract _iUserContract;

        public UserController(IUserContract iUserContract)
        {
            _iUserContract = iUserContract;
        }
	}
}
