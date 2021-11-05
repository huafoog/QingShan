using QingShan.DataLayer.Enums;
using System.ComponentModel.DataAnnotations;

namespace QingShan.Services.User.Dtos.InputDto
{
    /// <summary>
    /// 修改
    /// </summary>
    public class UserUpdateInputDto
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "请输入账号")]
        public string UserName { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public EAdministratorStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        public string RealName { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public RoleRequestDto[] RoleIds { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public long Version { get; set; }
    }
}
