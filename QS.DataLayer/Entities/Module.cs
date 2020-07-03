﻿using QS.Core.Entity;
using QS.Core.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace QS.DataLayer.Entities
{
    public class Module:EntityBaseById<int>
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
        /// 获取或设置 父节点树形路径，父级树链Id根据一定格式构建的字符串，形如："$1$,$3$,$4$,$7$"，编辑时更新
        /// </summary>
        [DisplayName("父节点树形路径")]
        public string TreePathString { get; set; }

        /// <summary>
        /// 获取 从根结点到当前结点的树形路径编号数组，由<see cref="TreePathString"/>属性转换，此属性仅支持在内存中使用
        /// </summary>
        [NotMapped]
        public int[] TreePathIds
        {
            get
            {
                return TreePathString?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Select(m => m.Trim('$').CastTo<int>()).ToArray() ?? new int[0];
            }
        }

        /// <summary>
        /// 获取或设置 父模块编号
        /// </summary>
        [DisplayName("父模块编号")]
        public int? ParentId { get; set; }
    }
}
