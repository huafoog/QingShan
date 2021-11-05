//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using QingShan.Data;

namespace Demo.Services.Permission.Dto
{
    /// <summary>
	/// 权限
    /// </summary>
	public class PermissionInputDto
    {

        /// <summary>
		/// 获取或设置 编号
        /// </summary>
        public System.String Id { get; set; }
        
        /// <summary>
		/// 菜单编码
        /// </summary>
        public System.String Code { get; set; }
        
        /// <summary>
		/// 组件
        /// </summary>
        public System.String Component { get; set; }
        
        /// <summary>
		/// 图标
        /// </summary>
        public System.String Icon { get; set; }
        
        /// <summary>
		/// 模块名称
        /// </summary>
        public System.String Name { get; set; }
        
        /// <summary>
		/// 父级id
        /// </summary>
        public System.String ParentId { get; set; }
        
        /// <summary>
		/// 路径
        /// </summary>
        public System.String Path { get; set; }
        
        /// <summary>
		/// 自动生成 权限代码 格式:system.menu.add
        /// </summary>
        public System.String PermissionCode { get; set; }
        
        /// <summary>
		/// 权限类型
        /// </summary>
        public System.Int32 PermissionType { get; set; }
        
        /// <summary>
		/// 备注信息
        /// </summary>
        public System.String Remark { get; set; }
        
        /// <summary>
		/// 排序值
        /// </summary>
        public System.Int32 Sort { get; set; }
        
    }
    /// <summary>
	/// 权限
    /// </summary>
    public class PagePermissionInputDto:PageInputDto
    {

    }
}
