using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        [Required(ErrorMessage ="请输入账号")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
    }
}
