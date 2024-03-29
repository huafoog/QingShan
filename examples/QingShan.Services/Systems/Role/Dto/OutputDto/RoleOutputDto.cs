﻿using QingShan.Attributes;
using QingShan.DataLayer.Entities;
using System;

namespace QingShan.Services.Systems.Role.Dto.OutputDto
{
    /// <summary>
    /// 角色输出参数
    /// </summary>
    public class RoleOutputDto
    {

        public string Id { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///描述
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        ///排序
        /// </summary>
        public int OrderSort { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }

        public DateTime CreatedTime { get; set; }
    }
}
