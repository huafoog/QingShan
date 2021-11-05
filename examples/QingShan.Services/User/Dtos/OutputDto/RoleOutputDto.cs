using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Services.User.Dtos.OutputDto
{
    public class RoleOutputDto
    {

        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }


        /// <summary>
        /// 角色id
        /// </summary>
        public string RoleId { get; set; }
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
    }
}
