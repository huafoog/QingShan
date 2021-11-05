using QingShan.DatabaseAccessor;
using QingShan.DataLayer.Enums;
using QingShan.Services.User.Dtos.InputDto;
using System;

namespace QingShan.Services.User.Dtos.OutputDto
{
    public class UserListOutputDto : IEntity
    {
        /// <summary>
        /// 主键Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// 账号
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public EAdministratorStatus Status { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime? CreatedTime { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 角色id
        /// </summary>
        public RoleRequestDto[] RoleIds { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public string RoleNames { get; set; }
    }
}
