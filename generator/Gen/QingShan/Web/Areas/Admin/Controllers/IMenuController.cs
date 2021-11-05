//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using System.Collections.Generic;
using System.Text;
using QingShan.Services.Menu;

namespace QingShan.Web.Areas.Admin.Controllers
{
    /// <summary>
	/// 菜单
    /// </summary>
	public class MenuController
	{

        private readonly IMenuContract _iMenuContract;

        public MenuController(IMenuContract iMenuContract)
        {
            _iMenuContract = iMenuContract;
        }
	}
}
