// -----------------------------------------------------------------------
//  <copyright file="FunctionService.cs" company="OSharp开源团队">
//      Copyright (c) 2014-2018 OSharp. All rights reserved.
//  </copyright>
//  <site>http://www.osharp.org</site>
//  <last-editor>郭明锋</last-editor>
//  <last-date>2018-06-23 17:23</last-date>
// -----------------------------------------------------------------------

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QS.Core.Attributes;
using QS.Core.Data.Enums;
using QS.Core.Dependency;
using QS.Core.Extensions;
using QS.Core.Permission.Authorization.Functions;
using QS.Core.Permission.Authorization.Modules;
using QS.Core.Reflection;
using QS.DataLayer.Entities;
using QS.ServiceLayer.Permission;
using QS.ServiceLayer.Permission.Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace QS.Permission
{
    /// <summary>
    /// 方法服务
    /// </summary>
    public class FunctionService:IFunctionService,IScopeDependency
    {
        private readonly List<IFunction> _functions = new List<IFunction>();

        private readonly IPermissionService _permissionService;
        private readonly IAssemblyFinder _assemblyFinder;

        private readonly MethodInfo[] _methods;
        public FunctionService(IPermissionService permissionService, IAssemblyFinder assemblyFinder)
        {
            _permissionService = permissionService;
            _assemblyFinder = assemblyFinder;
            _methods = _assemblyFinder.Find(o => o.FullName.Contains("QS.Core.Web")).FirstOrDefault()
                    .GetTypes().SelectMany(m => m.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                    .Where(type => type.HasAttribute<ModuleInfoAttribute>()).ToArray();
            _functions = PickupFunctions().ToList();
        }

        #region 方法
        public IFunction[] PickupFunctions()
        {
            var types = _assemblyFinder.Find(o => o.FullName.Contains("QS.Core.Web")).FirstOrDefault()
                    .GetTypes().Where(m => m.HasAttribute<ControllerAttribute>()).ToArray();
            return GetFunctions(types);
        }

        /// <summary>
        /// 从功能类型中获取功能信息
        /// </summary>
        /// <param name="functionTypes">功能类型集合</param>
        /// <returns></returns>
        protected IFunction[] GetFunctions(Type[] functionTypes)
        {
            List<IFunction> functions = new List<IFunction>();
            foreach (Type type in functionTypes.OrderBy(m => m.FullName))
            {
                IFunction controller = GetFunction(type);
                if (controller == null || type.HasAttribute<NonFunctionAttribute>())
                {
                    continue;
                }

                if (!HasPickup(functions, controller))
                {
                    functions.Add(controller);
                }

                List<MethodInfo> methods = _methods.ToList();
                // 移除已被重写的方法
                MethodInfo[] overriddenMethodInfos = methods.Where(m => m.IsOverridden()).ToArray();
                foreach (MethodInfo overriddenMethodInfo in overriddenMethodInfos)
                {
                    methods.RemoveAll(m => m.Name == overriddenMethodInfo.Name && m != overriddenMethodInfo);
                }

                foreach (MethodInfo method in methods)
                {
                    IFunction action = GetFunction(controller, method);
                    if (action == null)
                    {
                        continue;
                    }

                    if (IsIgnoreMethod(action, method, functions))
                    {
                        continue;
                    }

                    if (HasPickup(functions, action))
                    {
                        continue;
                    }

                    functions.Add(action);
                }
            }

            return functions.ToArray();
        }
        /// <summary>
        /// 从功能类型创建功能信息
        /// </summary>
        /// <param name="controllerType">功能类型</param>
        /// <returns></returns>
        protected IFunction GetFunction(Type controllerType)
        {
            if (!controllerType.IsController())
            {
                throw new Exception($"类型“{controllerType.FullName}”不是MVC控制器类型");
            }
            FunctionAccessType accessType = controllerType.HasAttribute<LoggedInAttribute>()
                ? FunctionAccessType.LoggedIn
                : FunctionAccessType.RoleLimit;
            IFunction function = new FunctionEntity()
            {
                Name = controllerType.GetDescription(),
                Area = GetArea(controllerType),
                Controller = controllerType.Name.Replace("ControllerBase", string.Empty).Replace("Controller", string.Empty),
                IsController = true,
                AccessType = accessType
            };
            return function;
        }
        /// <summary>
        /// 功能信息查找
        /// </summary>
        /// <param name="functions">功能信息集合</param>
        /// <param name="area">区域名称</param>
        /// <param name="controller">类型名称</param>
        /// <param name="action">方法名称</param>
        /// <param name="name">功能名称</param>
        /// <returns></returns>
        protected IFunction GetFunction(string area, string controller, string action, string name)
        {
            return _functions.FirstOrDefault(m =>
                string.Equals(m.Area, area, StringComparison.OrdinalIgnoreCase)
                && string.Equals(m.Controller, controller, StringComparison.OrdinalIgnoreCase)
                && string.Equals(m.Action, action, StringComparison.OrdinalIgnoreCase)
                && string.Equals(m.Name, name, StringComparison.OrdinalIgnoreCase));
        }

        /// <summary>
        /// 实现从方法信息中创建功能信息
        /// </summary>
        /// <param name="typeFunction">类功能信息</param>
        /// <param name="method">方法信息</param>
        /// <returns></returns>
        protected IFunction GetFunction(IFunction typeFunction, MethodInfo method)
        {
            FunctionAccessType accessType = method.HasAttribute<LoggedInAttribute>()
                ? FunctionAccessType.LoggedIn
                : FunctionAccessType.RoleLimit;
            IFunction function = new FunctionEntity()
            {
                Name = $"{typeFunction.Name}-{method.GetDescription()}",
                Area = typeFunction.Area,
                Controller = typeFunction.Controller,
                Action = method.Name,
                AccessType = accessType,
                IsController = false,
                IsAjax = true
            };
            return function;
        }

        /// <summary>
        /// 重写以实现是否忽略指定方法的功能信息
        /// </summary>
        /// <param name="action">要判断的功能信息</param>
        /// <param name="method">功能相关的方法信息</param>
        /// <param name="functions">已存在的功能信息集合</param>
        /// <returns></returns>
        protected bool IsIgnoreMethod(IFunction action, MethodInfo method, IEnumerable<IFunction> functions)
        {
            if (method.HasAttribute<NonActionAttribute>() || method.HasAttribute<NonFunctionAttribute>())
            {
                return true;
            }

            IFunction existing = GetFunction(action.Area, action.Controller, action.Action, action.Name);
            return existing != null && method.HasAttribute<HttpPostAttribute>();
        }
       

        /// <summary>
        /// 查找指定条件的功能信息
        /// </summary>
        /// <param name="area">区域</param>
        /// <param name="controller">控制器</param>
        /// <param name="action">功能方法</param>
        /// <returns>功能信息</returns>
        public IFunction GetFunction(string area, string controller, string action)
        {
            return _functions.FirstOrDefault(m =>
                string.Equals(m.Area, area, StringComparison.OrdinalIgnoreCase)
                && string.Equals(m.Controller, controller, StringComparison.OrdinalIgnoreCase)
                && string.Equals(m.Action, action, StringComparison.OrdinalIgnoreCase));
        }
        /// <summary>
        /// 从类型中获取功能的区域信息
        /// </summary>
        private static string GetArea(MemberInfo type)
        {
            AreaAttribute attribute = type.GetAttribute<AreaAttribute>();
            return attribute?.RouteValue;
        }

        /// <summary>
        /// 重写以判断指定功能信息是否已提取过
        /// </summary>
        /// <param name="functions">已提取功能信息集合</param>
        /// <param name="function">要判断的功能信息</param>
        /// <returns></returns>
        protected bool HasPickup(List<IFunction> functions, IFunction function)
        {
            return functions.Any(m =>
                string.Equals(m.Area, function.Area, StringComparison.OrdinalIgnoreCase)
                && string.Equals(m.Controller, function.Controller, StringComparison.OrdinalIgnoreCase)
                && string.Equals(m.Action, function.Action, StringComparison.OrdinalIgnoreCase)
                && string.Equals(m.Name, function.Name, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region 收集权限点
        /// <summary>
        /// 从程序集中获取模块信息
        /// </summary>
        public ModuleInfo[] Pickup()
        {
            Type[] moduleTypes = _assemblyFinder.Find(o => o.FullName.Contains("QS.Core.Web")).FirstOrDefault().GetTypes()
                .Where(type => type.HasAttribute<ModuleInfoAttribute>()).ToArray();
            ModuleInfo[] modules = GetModules(moduleTypes);
            return modules;
        }

        public ModuleInfo[] GetModules(Type[] moduleTypes)
        {
            List<ModuleInfo> infos = new List<ModuleInfo>();
            //if (moduleTypes != null && moduleTypes.Length > 0)
            //{
            //    string infoAttr = moduleTypes.FirstOrDefault().GetAttribute<AreaAttribute>()?.RouteValue;
            //    ModuleInfo info = new ModuleInfo()
            //    {
            //        Name = infoAttr,
            //        Code = infoAttr,
            //        Order = 1,
            //        ModuleType = ModuleType.Module
            //    };
            //    infos.AddIfNotExist(info);
            //}
            foreach (Type moduleType in moduleTypes)
            {
                string[] existPaths = infos.Select(m => $"{m.Position}.{m.Code}").ToArray();
                ModuleInfo[] typeInfos = GetModules(moduleType, existPaths);
                foreach (ModuleInfo info in typeInfos)
                {
                    if (info.Order == 0)
                    {
                        info.Order = infos.Count(m => m.Position == info.Position) + 1;
                    }

                    infos.AddIfNotExist(info);
                }
                MethodInfo[] methods = _assemblyFinder.Find(o => o.FullName.Contains("QS.Core.Web")).FirstOrDefault()
                    .GetTypes().SelectMany(m => m.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
                    .Where(type => type.HasAttribute<ModuleInfoAttribute>()).ToArray();
                for (int index = 0; index < methods.Length; index++)
                {
                    ModuleInfo methodInfo = GetModule(methods[index], typeInfos.Last(), index);
                    infos.AddIfNotNull(methodInfo);
                }
            }

            return infos.ToArray();
        }

        /// <summary>
        /// 从类型中提取模块信息
        /// </summary>
        /// <param name="type">类型信息</param>
        /// <param name="existPaths">已存在的路径集合</param>
        /// <returns>提取到的模块信息</returns>
        protected ModuleInfo[] GetModules(Type type, string[] existPaths)
        {
            ModuleInfoAttribute infoAttr = type.GetAttribute<ModuleInfoAttribute>();
            if (infoAttr == null)
            {
                return new ModuleInfo[0];
            }
            ModuleInfo info = new ModuleInfo()
            {
                Name = infoAttr.Name ?? GetName(type),
                Code = infoAttr.Code ?? type.Name.Replace("Controller", ""),
                Order = infoAttr.Order,
                Position = GetPosition(type, infoAttr.Position),
                PositionName = infoAttr.PositionName,
                ModuleType = ModuleType.Menu
            };
            List<ModuleInfo> infos = new List<ModuleInfo>() { info };
            //获取中间分类模块
            if (infoAttr.Position != null)
            {
                info = new ModuleInfo()
                {
                    Name = infoAttr.PositionName ?? infoAttr.Position,
                    Code = infoAttr.Position,
                    Position = GetPosition(type, null),
                    ModuleType = ModuleType.Module
                };
                if (!existPaths.Contains($"{info.Position}.{info.Code}"))
                {
                    infos.Insert(0, info);
                }
            }
            //获取区域模块
            string area, name;
            AreaInfoAttribute areaInfo = type.GetAttribute<AreaInfoAttribute>();
            if (areaInfo != null)
            {
                area = areaInfo.RouteValue;
                name = areaInfo.Display ?? area;
            }
            else
            {
                AreaAttribute areaAttr = type.GetAttribute<AreaAttribute>();
                area = areaAttr?.RouteValue ?? "Site";
                DisplayNameAttribute display = type.GetAttribute<DisplayNameAttribute>();
                name = display?.DisplayName ?? area;
            }
            info = new ModuleInfo()
            {
                Name = name,
                Code = area,
                Position = "",
            };
            if (!existPaths.Contains($"{info.Position}.{info.Code}"))
            {
                infos.Insert(0, info);
            }

            return infos.ToArray();
        }

        /// <summary>
        /// 从方法信息中提取模块信息
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <param name="typeInfo">所在类型模块信息</param>
        /// <param name="index">序号</param>
        /// <returns>提取到的模块信息</returns>
        protected ModuleInfo GetModule(MethodInfo method, ModuleInfo typeInfo, int index)
        {
            ModuleInfoAttribute infoAttr = method.GetAttribute<ModuleInfoAttribute>();
            if (infoAttr == null)
            {
                return null;
            }
            ModuleInfo info = new ModuleInfo()
            {
                Name = infoAttr.Name ?? method.GetDescription() ?? method.Name,
                Code = infoAttr.Code ?? method.Name,
                Order = infoAttr.Order > 0 ? infoAttr.Order : index + 1,
                ModuleType = ModuleType.Dot
            };
            string controller = method.DeclaringType?.Name.Replace("ControllerBase", string.Empty).Replace("Controller", string.Empty);
            info.Position = $"{typeInfo.Position}.{controller}";
            //依赖的功能
            string area = method.DeclaringType.GetAttribute<AreaAttribute>()?.RouteValue;
            List<IFunction> dependOnFunctions = new List<IFunction>()
            {
                GetFunction(area, controller, method.Name)
            };
            info.DependOnFunctions = dependOnFunctions.ToArray();
            return info;
        }

        private static string GetName(Type type)
        {
            string name = type.GetDescription();
            if (name == null)
            {
                return type.Name.Replace("Controller", "");
            }
            if (name.Contains("-"))
            {
                name = name.Split('-').Last();
            }
            return name;
        }

        private static string GetPosition(Type type, string attrPosition)
        {
            string area = type.GetAttribute<AreaAttribute>()?.RouteValue;
            if (area == null)
            {
                //无区域，使用Site位置
                return attrPosition == null
                    ? "Site"
                    : $"Site.{attrPosition}";
            }
            return attrPosition == null
                ? $"{area}"
                : $"{area}.{attrPosition}";
        }
        #endregion
    }
}
