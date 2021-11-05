using FreeSql.DataAnnotations;
using QingShan.DatabaseAccessor;

namespace QingShan.DataLayer.Entities
{
    /// <summary>
    /// 角色模型
    /// </summary>
    [Table(Name = "role")]
    public class RoleEntity : EntityBase
    {
        /// <summary>
        /// 角色名
        /// </summary>
        [Column(Name = "nvarchar(50)")]
        public string Name { get; set; }
        /// <summary>
        ///描述
        /// </summary>
        [Column(Name = "nvarchar(100)")]
        public string Description { get; set; }
        /// <summary>
        ///排序
        /// </summary>
        public int OrderSort { get; set; }
        /// <summary>
        /// 是否激活
        /// </summary>
        public bool Enabled { get; set; }
    }
}
