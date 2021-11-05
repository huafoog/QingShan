//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using QingShan.Data;

namespace Demo.Services.UserRole.Dto
{
    /// <summary>
	/// 用户角色实体
    /// </summary>
	public class UserRoleInputDto
    {

        /// <summary>
		/// 获取或设置 编号
        /// </summary>
        public System.String Id { get; set; }
        
        /// <summary>
		/// 角色Id
        /// </summary>
        public System.String RoleId { get; set; }
        
        /// <summary>
		/// 用户Id
        /// </summary>
        public System.String UserId { get; set; }
        
    }
    /// <summary>
	/// 用户角色实体
    /// </summary>
    public class PageUserRoleInputDto:PageInputDto
    {

    }
}
