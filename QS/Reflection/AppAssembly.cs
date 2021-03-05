using Microsoft.Extensions.DependencyModel;
using QS.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace QS
{
    /// <summary>
    /// 站点程序集
    /// </summary>
    public static class AppAssembly
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
        /// 静态构造函数，只在程序启动时执行一次。
        /// </summary>
        static AppAssembly()
        {
            Assemblies = GetAssemblies();
            CanBeScanTypes = Assemblies.SelectMany(u => u.GetTypes().Where(u => u.IsPublic && !u.IsDefined(typeof(SkipScanAttribute), false)));
        }

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

            // 读取项目程序集或 Furion 官方发布的包，或手动添加引用的dll
            var scanAssemblies = dependencyContext.CompileLibraries
                .Where(u => (u.Type == "project" && !excludeAssemblyNames.Any(j => u.Name.EndsWith(j)))
                    || (u.Type == "package" && u.Name.StartsWith(nameof(QS))))    // 判断是否启用引用程序集扫描
                .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)))
                .ToList();

            return scanAssemblies;
        }
    }
}
