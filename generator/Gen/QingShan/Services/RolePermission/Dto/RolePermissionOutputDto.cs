using System;

namespace QingShan.Services.RolePermission.Dto
{
    /// <summary>
	/// 角色模块
    /// </summary>
	public class RolePermissionOutputDto
    {

        /// <summary>
		/// 获取或设置 编号
        /// </summary>
        public System.String Id { get; set; }
        
        /// <summary>
		/// 权限id
        /// </summary>
        public System.String PermissionId { get; set; }
        
        /// <summary>
		/// 角色Id
        /// </summary>
        public System.String RoleId { get; set; }
        
    }
}
