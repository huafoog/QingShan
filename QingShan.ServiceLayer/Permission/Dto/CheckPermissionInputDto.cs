using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.Services.Permission.Dto
{
    /// <summary>
    /// 检查权限
    /// </summary>
    public class CheckPermissionInputDto
    {
        public string Area { get; set; }
        public string IsArea { get; set; }
        public string Controller { get; set; }
        public string Action { get; set; }
    }
}
