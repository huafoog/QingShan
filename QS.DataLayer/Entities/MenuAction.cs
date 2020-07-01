using QS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QS.DataLayer.Entities
{
    public class MenuAction:EntityBase<int>
    {
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        public int Code { get; set; }

        public List<string> Url { get; set; }
    }
}
