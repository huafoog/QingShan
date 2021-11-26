using System;
using QingShan.Data;
using FreeSql.DataAnnotations;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
	/// 用户表
    /// </summary>
	[Table(Name ="user")]
    public class UserEntity : QingShan.DatabaseAccessor.EntityBase
    {

            /// <summary>
		    /// 头像
            /// </summary>
            public System.String Avatar { get; set; }
            
            /// <summary>
		    /// 部门Id
            /// </summary>
            public System.String DepartmentId { get; set; }
            
            /// <summary>
		    /// 是否本系统超级管理员
            /// </summary>
            public System.Boolean IsSuper { get; set; }
            
            /// <summary>
		    /// 上次登录ip
            /// </summary>
            public System.String LastLoginIp { get; set; }
            
            /// <summary>
		    /// 上次登录时间
            /// </summary>
            public System.DateTime LastLoginTime { get; set; }
            
            /// <summary>
		    /// 昵称
            /// </summary>
            public System.String NickName { get; set; }
            
            /// <summary>
		    /// 密码            <para>两次加密 第一次32位小写 第二次32位大写</para>
            /// </summary>
            public System.String Password { get; set; }
            
            /// <summary>
		    /// 手机号码
            /// </summary>
            public System.String Phone { get; set; }
            
            /// <summary>
		    /// 真实姓名
            /// </summary>
            public System.String RealName { get; set; }
            
            /// <summary>
		    /// 个性签名
            /// </summary>
            public System.String Remark { get; set; }
            
            /// <summary>
		    /// 账号状态
            /// </summary>
            public System.Enum Status { get; set; }
            
            /// <summary>
		    /// 更新时间
            /// </summary>
            public System.DateTime UpdateDateTime { get; set; }
            
            /// <summary>
		    /// 用户名称            <para>登录账号</para>
            /// </summary>
            public System.String UserName { get; set; }
            
    }
}
