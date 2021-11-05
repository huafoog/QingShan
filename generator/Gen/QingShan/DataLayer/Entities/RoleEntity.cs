//QS Code Generation Template 1.0
//author:QS
//blog:www.cnblogs.com/qs315
//此代码由工具自动生成，请勿修改

using System;
using QingShan.Data;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
	/// 角色模型
    /// </summary>
	public class RoleEntity : QingShan.DatabaseAccessor.EntityBase
    {

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
