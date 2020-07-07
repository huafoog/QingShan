using QS.Core.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 接口管理
    /// </summary>
    public class ApiEntity:EntityBase<int>
    {
        /// <summary>
        /// 所属模块
        /// </summary>
		public long ParentId { get; set; }

        /// <summary>
        /// 接口命名
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        /// <summary>
        /// 接口名称
        /// </summary>
        [Column(TypeName = "nvarchar(500)")]
        public string Label { get; set; }

        /// <summary>
        /// 接口地址
        /// </summary>
        [Column(TypeName = "nvarchar(500)")]
        public string Path { get; set; }

        /// <summary>
        /// 接口提交方法
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string HttpMethods { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        [Column(TypeName = "nvarchar(500)")]
        public string Description { get; set; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { get; set; }

        /// <summary>
        /// 启用
        /// </summary>
        public bool Enabled { get; set; }
    }
}
