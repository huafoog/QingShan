﻿using System;
using QingShan.Data;

namespace QingShan.Services.Menu.Dto
{
    /// <summary>
	/// 菜单
    /// </summary>
	public class MenuInputDto
    {

        /// <summary>
		/// 获取或设置 编号
        /// </summary>
        public System.String Id { get; set; }
        
        /// <summary>
		/// 菜单code
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
		/// 菜单名称
        /// </summary>
        public System.String Name { get; set; }
        
        /// <summary>
		/// 父级id
        /// </summary>
        public System.String ParentId { get; set; }
        
        /// <summary>
		/// 跳转地址
        /// </summary>
        public System.String Redirect { get; set; }
        
    }
    /// <summary>
	/// 菜单
    /// </summary>
    public class PageMenuInputDto:PageInputDto
    {

    }
}
