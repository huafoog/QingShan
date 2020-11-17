using QS.Core.Entity;

namespace QS.DataLayer.Entities
{
    /// <summary>
    /// 用户角色实体
    /// </summary>
    public class UserRoleEntity : EntityBase<int>
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public int RoleId { get; set; }
    }
}
