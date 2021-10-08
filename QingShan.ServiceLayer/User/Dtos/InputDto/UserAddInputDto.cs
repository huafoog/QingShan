using QingShan.Attributes;
using QingShan.DataLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace QingShan.Services.User.Dtos.InputDto
{
    /// <summary>
    /// 添加
    /// </summary>
    public class UserAddInputDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "请输入账号")]
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [Required(ErrorMessage = "请输入手机号")]
        [RegularExpression(Regular.RegularPatterns.Phone,ErrorMessage = "请输入正确的手机号")]
        public string Phone { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [Required(ErrorMessage = "请输入昵称")] 
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Required(ErrorMessage = "请输入选择头像")]
        public string Avatar { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public RoleRequestDto[] RoleIds { get; set; }
    }

    public class RoleRequestDto
    {

        /// <summary>
        /// 角色id
        /// </summary>
        public string key { get; set; }

        public string label { get; set; }

        public string value { get; set; }
    }
}
