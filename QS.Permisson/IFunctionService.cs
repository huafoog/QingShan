using QS.DataLayer.Entities;
using QS.Permission.Modules;
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
        FunctionEntity[] PickupFunctions();
    }
}
