//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using QingShan.Data;

namespace QingShan.Services.RolePermission.Dto
{
    /// <summary>
	/// 角色模块
    /// </summary>
	public class RolePermissionInputDto
    {

        /// <summary>
		/// 获取或设置 编号
        /// </summary>
        public System.String Id { get; set; }
        
        /// <summary>
		/// 创建人id
        /// </summary>
        public System.String CreatedId { get; set; }
        
        /// <summary>
		/// 获取或设置 创建时间
        /// </summary>
        public System.DateTime CreateTime { get; set; }
        
        /// <summary>
		/// 获取或设置 数据状态
        /// </summary>
        public System.DateTime DeleteTime { get; set; }
        
        /// <summary>
		/// 权限id
        /// </summary>
        public System.String PermissionId { get; set; }
        
        /// <summary>
		/// 角色Id
        /// </summary>
        public System.String RoleId { get; set; }
        
    }
    /// <summary>
	/// 角色模块
    /// </summary>
    public class PageRolePermissionInputDto:PageInputDto
    {

    }
}
