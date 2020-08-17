using Microsoft.AspNetCore.Mvc;
using QS.Core.Data.Enums;
using QS.Core.Dependency;
using QS.Core.Extensions;
using QS.Core.Helper;
using QS.Core.Permission.Authorization;
using QS.Core.Reflection;
using QS.ServiceLayer.Permission.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace QS.Core.Web.Authorization
{
    /// <summary>
    /// 模块管理
    /// </summary>
    public class ModuleManager: IModuleManager, ISingletonDependency
    {
        private readonly IAssemblyFinder _assemblyFinder;
        public ModuleManager(IAssemblyFinder assemblyFinder)
        {
            _assemblyFinder = assemblyFinder;
        }

        private List<ModuleInfo> moduleInfos = new List<ModuleInfo>();
        /// <summary>
        /// 收集模块
        /// </summary>
        public List<ModuleInfo> GetModules()
        {
            //当前所有权限
            var types = RuntimeHelper.GetAllTypes();
            //获取需要模块信息
            var typeInfos = types.Where(o => o.IsClass &&  o.GetCustomAttributes(false).Any(p => p is ModuleInfoAttribute));
            List<ModuleInfo> infos = new List<ModuleInfo>();
            //循环类
            foreach (var moduleType in typeInfos)
            {
                var module = GetModule(moduleType);
                infos.AddRange(module);

                //当前方法查询操作信息
                List<MethodInfo> methods = moduleType.GetMethods().Where(type => type.HasAttribute<ModuleInfoAttribute>()).ToList();
                for (int index = 0; index < methods.Count; index++)
                {
                    ModuleInfo methodInfo = GetModule(methods[index], module.Last(),moduleType, index);
                    infos.Add(methodInfo);
                }
            }
            return infos;
        }

        #region 信息组装
        /// <summary>
        /// 获取当前类中所有数据
        /// </summary>
        /// <param name="type"></param>
        private List<ModuleInfo> GetModule(Type type)
        {
            var modules = new List<ModuleInfo>();
            ModuleInfoAttribute infoAttr = type.GetAttribute<ModuleInfoAttribute>();
            if (infoAttr == null)
            {
                return new List<ModuleInfo>();
            }
            if (infoAttr.URL == null)
            {
                throw new Exception($"请在类名为【{type.Name}】的特性ModuleInfo中输入URL");
            }

            if (infoAttr.Module == ModuleEnum.Null)
            {
                throw new Exception($"请在类名为【{type.Name}】的特性ModuleInfo中输入【Module】参数");
            }
            ModuleInfo moduleInfo = moduleInfos.FirstOrDefault(o => o.Code == infoAttr.Code);
            if (moduleInfo == null)
            {
                moduleInfo = new ModuleInfo()
                {
                    Code = infoAttr.Module.ToString(),
                    Name = infoAttr.Module.ToDescription(),
                    Id = Guid.NewGuid(),
                    Level = ModuleLevel.Module,
                    Sort = (int)infoAttr.Module,
                    Pid = Guid.Empty,
                    Path = infoAttr.P_Url ?? ""
                };
                modules.Add(moduleInfo);
            }
            ModuleInfo info = new ModuleInfo()
            {
                Code = infoAttr.Code ?? type.Name,
                Name = infoAttr.Name ?? GetName(type),
                Id = Guid.NewGuid(),
                Level = ModuleLevel.Menu,
                Sort = infoAttr.Sort,
                Pid = moduleInfo.Id,
                Path = infoAttr.P_Url
            };
            modules.Add(info);
            moduleInfos.AddRange(modules.ToList());
            return modules;
        }

        /// <summary>
        /// 重写以实现从方法信息中提取模块信息
        /// </summary>
        /// <param name="method">方法信息</param>
        /// <param name="moduleInfo">所在类型模块信息</param>
        /// <param name="index">序号</param>
        /// <returns>提取到的模块信息</returns>
        protected ModuleInfo GetModule(MethodInfo method, ModuleInfo moduleInfo,Type type, int index)
        {
            ModuleInfoAttribute infoAttr = method.GetAttribute<ModuleInfoAttribute>();
            if (infoAttr == null)
            {
                return null;
            }
            AreaAttribute areaAttr = method.GetAttribute<AreaAttribute>();
            var area = areaAttr?.RouteValue ?? "Admin";
            ModuleInfo info = new ModuleInfo()
            {
                Code = infoAttr.Code ?? method.Name,
                Name = infoAttr.Name ?? GetName(method),
                Id = Guid.NewGuid(),
                Level = ModuleLevel.Function,
                Path = infoAttr.URL,
                Pid = moduleInfo.Id,
                Sort = infoAttr.Sort > 0 ? infoAttr.Sort : index + 1,
                PCode = moduleInfo.Code,
                Action = method.Name,
                Controller = type.Name.Replace("ControllerBase", string.Empty).Replace("Controller", string.Empty),
                Area = area
            };
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
        private static string GetName(MethodInfo type)
        {
            string name = type.GetDescription();
            if (name == null)
            {
                return type.Name;
            }
            return name;
        }
        #endregion
    }
}
