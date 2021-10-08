using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyModel;
using Microsoft.Extensions.Options;
using QingShan.DependencyInjection;
using QingShan.Core.Options;
using StackExchange.Profiling;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading;
using QingShan.Utilities;
using QingShan.Attributes;

namespace QingShan.Core
{
    /// <summary>
    /// 全局应用类
    /// </summary>
    public static class App
    {

        /// <summary>
        /// 应用有效程序集
        /// </summary>
        public static readonly IEnumerable<Assembly> Assemblies;

        /// <summary>
        /// 能够被扫描的类型
        /// </summary>
        public static readonly IEnumerable<Type> CanBeScanTypes;


        /// <summary>
        /// 只需要登录权限的code
        /// </summary>
        public static readonly IEnumerable<string> LoggedCodes;

        /// <summary>
        /// 静态构造函数，只在程序启动时执行一次。
        /// </summary>
        static App()
        {
            // 编译配置
            Assemblies = GetAssemblies();
            CanBeScanTypes = Assemblies.SelectMany(u => u.GetTypes().Where(u => u.IsPublic && !u.IsDefined(typeof(SkipScanAttribute), false)));
            AppStartups = new ConcurrentBag<AppStartup>();

            LoggedCodes = GetLoggedInCode();

        }

        /// <summary>
        /// 获取登录后访问的权限
        /// </summary>
        /// <returns></returns>
        private static List<string> GetLoggedInCode()
        {
            List<string> code = new();
            //所有的控制器
            var controllers = CanBeScanTypes.Where(u=>u.IsController());
            foreach (var controller in controllers)
            {
                var areaName = "";
                //区域代码
                var area = controller.GetAttribute<AreaInfoAttribute>();
                if (area != null)
                {
                    areaName = $"{area.RouteValue}.";
                }


                var isLogged = controller.GetType().IsDefined(typeof(LoggedInAttribute));
                var actions = controller.GetMethods().Where(o => o.IsAction(controller));
                foreach (var action in actions)
                {
                    if (isLogged)
                    {
                        code.Add($"{areaName}{controller.Name.Replace("Controller", "")}.{action.Name}".ToLower());
                        continue;
                    }
                    if (action.IsDefined(typeof(LoggedInAttribute)))
                    {
                        code.Add($"{areaName}{controller.Name.Replace("Controller", "")}.{action.Name}".ToLower());
                    }
                }

            }
            return code;
        }

        /// <summary>
        /// 应用所有启动配置对象
        /// </summary>
        internal static ConcurrentBag<AppStartup> AppStartups;


        #region 选项
        #endregion

        #region 服务

        #endregion



        /// <summary>
        /// 获取应用有效程序集
        /// </summary>
        /// <returns>IEnumerable</returns>
        private static IEnumerable<Assembly> GetAssemblies()
        {
            // 需排除的程序集后缀
            var excludeAssemblyNames = new string[] {
                "Database.Migrations"
            };


            var dependencyContext = DependencyContext.Default;

            var scanAssemblies = dependencyContext.CompileLibraries
                .Where(u => (u.Type == "project" && !excludeAssemblyNames.Any(j => u.Name.EndsWith(j)))
                    || (u.Type == "package" && u.Name.StartsWith(nameof(QingShan))))    // 判断是否启用引用程序集扫描
                .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)))
                .ToList();

            return scanAssemblies;
        }

    }
}
