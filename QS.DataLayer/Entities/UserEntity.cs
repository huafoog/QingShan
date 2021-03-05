using QS.DatabaseAccessor;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 用户表
    /// </summary>
    public class UserEntity : EntityBase<int>
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
        public int DepartmentId { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 密码
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
        /// 更新时间
        /// </summary>
        public DateTime? UpdateDateTime { get; set; }
    }
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum EAdministratorStatus
    {
        [Description("已停用")]
        Stop = -1,
        [Description("正常")]
        Normal = 0,
        [Description("未激活")]
        NotActive = 1,
    }
}
