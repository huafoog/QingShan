using System.ComponentModel.DataAnnotations;

namespace QS.ServiceLayer.User.Dtos.InputDto
{
    /// <summary>
    /// 更新基本信息
    /// </summary>
    public class UserUpdateBasicInputDto
    {
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }
        /// <summary>
        /// 主键Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Required(ErrorMessage = "请输入昵称")]
        public string NickName { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 版本
        /// </summary>
        public long Version { get; set; }
    }
}
