using QingShan.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QingShan.DataLayer.Enums
{
    /// <summary>
    /// 用户状态
    /// </summary>
    public enum EAdministratorStatus
    {
        [Description("已停用")]
        Stop = -1,
        [Description("正常")]
        Normal = 0,
        [Description("未激活")]
        NotActive = 1,
    }
}
