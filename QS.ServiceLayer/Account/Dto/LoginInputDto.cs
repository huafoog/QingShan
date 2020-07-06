using System;
using System.Collections.Generic;
using System.Text;

namespace QS.ServiceLayer.Account.Dto
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class LoginInputDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }
    }
}
