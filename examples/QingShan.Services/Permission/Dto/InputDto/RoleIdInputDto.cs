using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Services.Permission.Dto
{
    /// <summary>
    /// 角色id
    /// </summary>
    public class RoleIdInputDto
    {
        /// <summary>
        /// 角色id
        /// </summary>
        [Required(ErrorMessage = "未获取到角色信息")]
        public string RoleId { get; set; }
    }
}
