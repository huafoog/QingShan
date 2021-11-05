using QingShan.DatabaseAccessor;
using QingShan.DataLayer.Enums;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
    /// 用户表
    /// </summary>
    [FreeSql.DataAnnotations.Table(Name = "user")]
    public class UserEntity : EntityBase
    {
        /// <summary>
        /// 真实姓名
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string RealName { get; set; }

        /// <summary>
        /// 用户名称
        /// <para>登录账号</para>
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string UserName { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string NickName { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        [Column(TypeName = "nvarchar(500)")]
        public string Avatar { get; set; }

        /// <summary>
        /// 部门Id
        /// </summary>
        public string DepartmentId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 密码
        /// <para>两次加密 第一次32位小写 第二次32位大写</para>
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 账号状态 
        /// </summary>
        public EAdministratorStatus Status { get; set; }

        /// <summary>
        /// 是否本系统超级管理员
        /// </summary>
        public bool IsSuper { get; set; }

        /// <summary>
        /// 个性签名
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Remark { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }

        /// <summary>
        /// 上次登录ip
        /// </summary>
        public string LastLoginIp { get; set; }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public DateTime? LastLoginTime { get; set; }
    }
}
