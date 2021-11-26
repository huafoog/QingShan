using System;
using QingShan.Data;

namespace QingShan.Services.UserRole.Dto
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
