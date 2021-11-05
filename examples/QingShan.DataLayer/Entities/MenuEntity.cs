using QingShan.DatabaseAccessor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuEntity: EntityBase
    {
        /// <summary>
        /// 菜单code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 跳转地址
        /// </summary>
        public string Redirect { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 组件
        /// </summary>
        public string Component { get; set; }

        /// <summary>
        /// 父级id
        /// </summary>
        public string ParentId { get; set; }
    }
}
