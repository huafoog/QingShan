using QS.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 路由菜单表
    /// </summary>
    public class PermissionModel:EntityBase<int>
    {
        /// <summary>
        /// 菜单执行Action名
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Code { get; set; }
        /// <summary>
        /// 菜单显示名（如用户页、编辑(按钮)、删除(按钮)）
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Name { get; set; }
        /// <summary>
        /// 是否是按钮
        /// </summary>
        public bool IsButton { get; set; } = false;
        /// <summary>
        /// 是否是隐藏菜单
        /// </summary>
        public bool? IsHide { get; set; } = false;
        /// <summary>
        /// 是否keepAlive
        /// </summary>
        public bool? IskeepAlive { get; set; } = false;
        /// <summary>
        /// 按钮事件
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Func { get; set; }
        /// <summary>
        /// 上一级菜单（0表示上一级无菜单）
        /// </summary>
        public int Pid { get; set; }
        /// <summary>
        /// 接口api
        /// </summary>
        public int Mid { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        public int OrderSort { get; set; }
        /// <summary>
        /// 菜单图标
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Icon { get; set; }
        /// <summary>
        /// 菜单描述    
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }
        /// <summary>
        /// 激活状态
        /// </summary>
        public bool Enabled { get; set; }
    }
}
