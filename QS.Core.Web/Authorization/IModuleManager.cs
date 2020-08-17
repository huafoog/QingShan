using QS.ServiceLayer.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Text;

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
