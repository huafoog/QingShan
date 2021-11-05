//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;

namespace QingShan.Services.Role.Dto
{
    /// <summary>
	/// 角色模型
    /// </summary>
	public class RoleOutputDto
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
}
