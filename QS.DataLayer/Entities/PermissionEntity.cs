using QS.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 权限实体
    /// </summary>
    public class PermissionEntity:EntityBase<int>
    {
        /// <summary>
        /// 父级节点
        /// </summary>
        public long ParentId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        [Column(TypeName = "nvarchar(50)")]
        public string Label { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        [Column(TypeName = "nvarchar(500)")]
        public string Code { get; set; }

        /// <summary>
        /// 权限类型
        /// </summary>
        public PermissionType Type { get; set; }

        /// <summary>
        /// 视图
        /// </summary>
        public long? ViewId { get; set; }

        /// <summary>
        /// 接口
        /// </summary>
        public long? ApiId { get; set; }

        /// <summary>
        /// 菜单访问地址
        /// </summary>
        [Column(TypeName = "nvarchar(500)")]
        public string Path { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Icon { get; set; }

        /// <summary>
        /// 隐藏
        /// </summary>
		public bool Hidden { get; set; } = false;

        /// <summary>
        /// 启用
        /// </summary>
		public bool Enabled { get; set; } = true;

        /// <summary>
        /// 可关闭
        /// </summary>
        public bool? Closable { get; set; }

        /// <summary>
        /// 打开组
        /// </summary>
        public bool? Opened { get; set; }

        /// <summary>
        /// 打开新窗口
        /// </summary>
        public bool? NewWindow { get; set; }

        /// <summary>
        /// 链接外显
        /// </summary>
        public bool? External { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int? Sort { get; set; } = 0;

        /// <summary>
        /// 描述
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }
    }

    /// <summary>
    /// 权限类型
    /// </summary>
    public enum PermissionType
    {
        /// <summary>
        /// 分组
        /// </summary>
        Group = 1,
        /// <summary>
        /// 菜单
        /// </summary>
        Menu = 2,
        /// <summary>
        /// 接口
        /// </summary>
        Api = 3,
        /// <summary>
        /// 权限点
        /// </summary>
        Dot = 4
    }
}
