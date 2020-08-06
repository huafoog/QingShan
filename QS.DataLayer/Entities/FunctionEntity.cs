using QS.Core.Data.Enums;
using QS.Core.Entity;
using QS.Core.Permission.Authorization.Functions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 功能信息
    /// <para>这里主键使用Guid</para>
    /// </summary>
    public class FunctionEntity:IFunction,IDataState
    {

        public FunctionEntity()
        {
            Id = Guid.NewGuid();
        }

        /// <summary>
        /// 功能名称
        /// </summary>
        [DisplayName("名称")]
        [Column(TypeName = "nvarchar(100)")]
        public string Name { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        [DisplayName("区域")]
        [Column(TypeName = "nvarchar(100)")]
        public string Area { get; set; }

        /// <summary>
        /// 控制器名称
        /// </summary>
        [DisplayName("控制器")]
        [Column(TypeName = "nvarchar(100)")]
        public string Controller { get; set; }

        /// <summary>
        /// 控制器的功能名称
        /// </summary>
        [DisplayName("功能")]
        [Column(TypeName = "nvarchar(100)")]
        public string Action { get; set; }

        /// <summary>
        /// 方法编码
        /// </summary>
        public string FunctionCode { get; set; }

        /// <summary>
        /// 是否是控制器
        /// </summary>
        [DisplayName("是否控制器")]
        public bool IsController { get; set; }

        /// <summary>
        /// 是否Ajax功能
        /// </summary>
        [DisplayName("是否Ajax功能")]
        public bool IsAjax { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        [DisplayName("菜单图标")]
        [Column(TypeName = "nvarchar(100)")]
        public string Icon { get; set; }

        /// <summary>
        /// 访问类型
        /// </summary>
        [DisplayName("访问类型")]
        public FunctionAccessType AccessType { get; set; }

        /// <summary>
        /// 获取或设置 是否锁定
        /// </summary>
        [DisplayName("是否锁定")]
        public bool IsLocked { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [Column(TypeName = "nvarchar(100)")]
        public string Description { get; set; }
        public Guid Id { get; set; }
        public DataState DataState { get; set; }
    }
}
