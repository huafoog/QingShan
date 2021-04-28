using QingShan.DatabaseAccessor;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
    /// 用户角色实体
    /// </summary>
    public class UserRoleEntity : EntityBase
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// 角色Id
        /// </summary>
        public string RoleId { get; set; }
    }
}
