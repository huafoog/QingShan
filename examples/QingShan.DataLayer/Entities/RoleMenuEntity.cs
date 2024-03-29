﻿using FreeSql.DataAnnotations;
using QingShan.DatabaseAccessor;
using System;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
    /// 角色菜单
    /// </summary>
    [Table(Name = "role_menu")]
    public class RoleMenuEntity : EntityBase
    {
        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }

        /// <summary>
        /// 菜单id
        /// </summary>
        public string MeunId { get; set; }
    }
}
