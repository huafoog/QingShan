using QS.Core.Entity;
using QS.Core.Extensions;
using QS.Core.Permission.Authorization.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 模型实体
    /// </summary>
    public class ModuleEntity:EntityBaseById<int>, IDataState
    {
        /// <summary>
        /// 获取或设置 模块名称
        /// </summary>
        [Required, DisplayName("模块名称")]
        public string Name { get; set; }

        /// <summary>
        /// 获取或设置 备注
        /// </summary>
        [DisplayName("模块描述")]
        public string Remark { get; set; }

        /// <summary>
        /// 获取或设置 模块代码
        /// </summary>
        [Required]
        [DisplayName("模块代码")]
        public string Code { get; set; }

        /// <summary>
        /// 获取或设置 节点内排序码
        /// </summary>
        [DisplayName("排序码")]
        public double OrderCode { get; set; }

        /// <summary>
        /// 获取或设置 权限路径 形如："code.code.code"，
        /// </summary>
        [DisplayName("权限路径")]
        public string CodePath { get; set; }

        /// <summary>
        /// 获取或设置 父模块编号
        /// </summary>
        [DisplayName("父模块编号")]
        public int? ParentId { get; set; }
        public DataState DataState { get; set; }

        /// <summary>
        /// 模块类型 模块类型可能为模块、菜单、权限点
        /// </summary>
        public ModuleType ModuleType { get; set; }
    }
}
