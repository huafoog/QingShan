using QS.Core.Permission.Authorization.Functions;
using QS.Core.Permission.Authorization.Modules;
using QS.DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace QS.Permission
{
    public interface IFunctionService
    {

        /// <summary>
        /// 从程序集中获取模块信息
        /// </summary>
        ModuleInfo[] Pickup();

        /// <summary>
        /// 提取所有方法信息
        /// </summary>
        /// <param name="functionTypes">功能类型集合</param>
        /// <returns></returns>
        IFunction[] PickupFunctions();
    }
}
