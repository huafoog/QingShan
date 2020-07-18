﻿using QS.Core.Attributes;
using QS.DataLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace QS.ServiceLayer.User.Dtos.InputDto
{
    /// <summary>
    /// 添加
    /// </summary>
    [MapTo(typeof(UserEntity))]
    public class UserAddInputDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        [Required(ErrorMessage = "请输入账号")]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required(ErrorMessage = "请输入密码")]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName { get; set; }

        ///// <summary>
        ///// 头像
        ///// </summary>
        //public string Avatar { get; set; }

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
        public int[] RoleIds { get; set; }
    }
}
