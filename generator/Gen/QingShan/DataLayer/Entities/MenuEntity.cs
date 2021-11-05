//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using QingShan.Data;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
	/// 菜单
    /// </summary>
	public class MenuEntity : QingShan.DatabaseAccessor.EntityBase
    {

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
}
