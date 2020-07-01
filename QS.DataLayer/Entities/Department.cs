using QS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Department:EntityBase<int>
    {
        /// <summary>
        /// 部门名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Order { get; set; }

        /// <summary>
        /// 父ID
        /// </summary>
        public int ParentId { get; set; }
    }
}
