using QS.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 接口API地址信息表
    /// </summary>
    public class ModulesModel:EntityBase<int>
    {
        /// <summary>
        /// 父ID
        /// </summary>
        public int? ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        /// <summary>
        /// 菜单链接地址
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string LinkUrl { get; set; }
        /// <summary>
        /// 区域名称
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Area { get; set; }
        /// <summary>
        /// 控制器名称
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Controller { get; set; }
        /// <summary>
        /// Action名称
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Action { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Icon { get; set; }
        /// <summary>
        /// 菜单编号
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }
        /// <summary>
        /// /描述
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }
        /// <summary>
        /// 是否是右侧菜单
        /// </summary>
        public bool IsMenu { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }
    }
}
