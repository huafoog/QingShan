using QS.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 角色模型
    /// </summary>
    public class RoleModel:EntityBase<int>
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        /// <summary>
        ///描述
        /// </summary>
        [Column(TypeName  = "nvarchar(100)")]
        public string Description { get; set; }
        /// <summary>
        ///排序
        /// </summary>
        public int OrderSort { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }
    }
}
