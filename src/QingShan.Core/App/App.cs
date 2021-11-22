using Microsoft.Extensions.DependencyModel;
using QingShan.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using QingShan.Attributes;
using Microsoft.Extensions.Configuration;
using QingShan.Core.ConfigurableOptions;
using QingShan.Exceptions;

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
        /// 配置
        /// <para>使用依赖注入时添加</para>
        /// </summary>
        public static IConfiguration Configuration { get; set; }

        /// <summary>
        /// 静态构造函数，只在程序启动时执行一次。
        /// </summary>
        static App()
        {
            // 编译配置
            Assemblies = GetAssemblies();
            CanBeScanTypes = Assemblies.SelectMany(u => u.GetTypes().Where(u => u.IsPublic && !u.IsDefined(typeof(SkipScanAttribute), false)));

            LoggedCodes = GetLoggedInCode();
        }

        /// <summary>
        /// 直接获取选项配置
        /// <para>该方式未通过依赖注入实现，无法使用热更新</para>
        /// <para></para>
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public static TOptions GetDefultOptions<TOptions>(string key = null)
            where TOptions : IConfigurableOptions, new()
        {
            if (Configuration == null)
            {
                throw new QingShanException("您需要在服务中添加`services.AddConfigurable(Configuration);`");
            }
            return Configuration.GetDefultOptions<TOptions>();
        }

        /// <summary>
        /// 获取项目程序集
        /// </summary>
        /// <returns>IEnumerable</returns>
        private static IEnumerable<Assembly> GetAssemblies()
        {
            var list = new List<Assembly>();
            var deps = DependencyContext.Default;
            list = deps.CompileLibraries.Where(lib => !lib.Serviceable && lib.Type != "package" && lib.Type == "project")
                    .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)))
                    .ToList();//排除所有的系统程序集、Nuget下载包
            return list;
        }

        /// <summary>
        /// 获取登录后访问的权限
        /// </summary>
        /// <returns></returns>
        private static List<string> GetLoggedInCode()
        {
            List<string> code =  new List<string>();
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

    }
}
