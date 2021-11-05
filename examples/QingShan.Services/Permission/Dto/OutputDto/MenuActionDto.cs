using System;
using System.Collections.Generic;

namespace QingShan.Services.Permission.Dto
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class MenuActionDto
    {

        public int Id { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }

        public List<string> Url { get; set; }

        /// <summary>
        /// 权限值
        /// </summary>
        public int Code { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}
