using QS.Core.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 用户角色实体
    /// </summary>
    public class UserRoleEntity:EntityBase<int>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public long UserId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public long RoleId { get; set; }
    }
}
