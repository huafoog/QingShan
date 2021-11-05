using QingShan.Services.Permission.Dto.OutputDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Services.User.Dtos.OutputDto
{
    /// <summary>
    /// 用户权限输出参数
    /// </summary>
    public class UserPermissionOutputDto
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public UserGetOutputDto UserInfo { get; set; }

        /// <summary>
        /// 权限
        /// </summary>
        public List<string> Codes { get; set; }

        public List<string> Menu { get; set; }
    }
}
