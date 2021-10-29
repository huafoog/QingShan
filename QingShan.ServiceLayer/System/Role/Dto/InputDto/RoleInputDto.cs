using QingShan.Attributes;
using QingShan.DataLayer.Entities;
using System.ComponentModel.DataAnnotations;

namespace QingShan.Services.System.Role.Dto.InputDto
{
    /// <summary>
    /// 角色
    /// </summary>
    public class RoleInputDto
    {
        public string Id { get; set; }

        /// <summary>
        /// 角色名
        /// </summary>
        [Required(ErrorMessage = "请输入角色名称")]
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
    }
}
