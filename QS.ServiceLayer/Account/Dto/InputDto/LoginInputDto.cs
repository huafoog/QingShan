using System.ComponentModel.DataAnnotations;

namespace QS.Services.Account.Dto
{
    /// <summary>
    /// 用户登录
    /// </summary>
    public class LoginInputDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "请输入账号")]
        public string Account { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }
    }
}
