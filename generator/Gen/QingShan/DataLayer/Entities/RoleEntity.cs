using System;
using QingShan.Data;
using FreeSql.DataAnnotations;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
	/// 角色模型
    /// </summary>
	[Table(Name ="role")]
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
