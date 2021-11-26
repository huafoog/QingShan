using System;
using QingShan.Data;

namespace QingShan.Services.Role.Dto
{
    /// <summary>
	/// 角色模型
    /// </summary>
	public class RoleInputDto
    {

        /// <summary>
		/// 获取或设置 编号
        /// </summary>
        public System.String Id { get; set; }
        
        /// <summary>
		/// 描述
        /// </summary>
        public System.String Description { get; set; }
        
        /// <summary>
		/// 是否激活
        /// </summary>
        public System.Boolean Enabled { get; set; }
        
        /// <summary>
		/// 角色名
        /// </summary>
        public System.String Name { get; set; }
        
        /// <summary>
		/// 排序
        /// </summary>
        public System.Int32 OrderSort { get; set; }
        
    }
    /// <summary>
	/// 角色模型
    /// </summary>
    public class PageRoleInputDto:PageInputDto
    {

    }
}
