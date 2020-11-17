using QS.ServiceLayer.Permission.Dto;
using System.Collections.Generic;

namespace QS.Core.Web.Authorization
{
    public interface IModuleManager
    {
        /// <summary>
        /// 收集模块信息
        /// </summary>
        /// <returns></returns>
        List<ModuleInfo> GetModules();
    }
}
