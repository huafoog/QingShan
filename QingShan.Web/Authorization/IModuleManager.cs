using QingShan.Services.Permission.Dto;
using System.Collections.Generic;

namespace QingShan.Core.Web.Authorization
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
