using QS.Core.Data.Modules;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace QS.ServiceLayer.Permission
{
    /// <summary>
    /// 权限服务
    /// </summary>
    public interface  IPermissionService
    {
        /// <summary>
        /// 修改权限信息
        /// </summary>
        /// <param name="moduleInfos"></param>
        /// <returns></returns>
        Task UpdatePermissionAsync(ModuleInfo[] moduleInfos);
    }
}
