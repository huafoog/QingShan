using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using QS.Core.Options;
using QS.Core.Utility;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.DependencyModel;
using System.Runtime.Loader;

namespace QS.Core
{
    /// <summary>
    /// 全局应用类
    /// </summary>
    public static class App
    {


        /// <summary>
        /// 私有设置，避免重复解析
        /// </summary>
        private static AppSettingsOptions _settings;

        /// <summary>
        /// 应用全局配置
        /// </summary>
        public static AppSettingsOptions Settings
        {
            // 避免重复解析
            get
            {
                if (_settings == null)
                    _settings = GetDuplicateOptions<AppSettingsOptions>();
                return _settings;
            }
        }

        /// <summary>
        /// 全局配置选项
        /// </summary>
        public static readonly IConfiguration Configuration;

        /// <summary>
        /// 私有环境变量，避免重复解析
        /// </summary>
        private static IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// 应用环境，如，是否是开发环境，生产环境等
        /// </summary>
        public static IWebHostEnvironment WebHostEnvironment
        {
            get
            {
                if (_webHostEnvironment == null)
                    _webHostEnvironment = GetDuplicateService<IWebHostEnvironment>();
                return _webHostEnvironment;
            }
        }

        /// <summary>
        /// 应用有效程序集
        /// </summary>
        public static readonly IEnumerable<Assembly> Assemblies;

        /// <summary>
        /// 能够被扫描的类型
        /// </summary>
        public static readonly IEnumerable<Type> CanBeScanTypes;


        /// <summary>
        /// 瞬时服务提供器，每次都是不一样的实例
        /// </summary>
        public static IServiceProvider ServiceProvider => InternalApp.InternalServices.BuildServiceProvider();



        static App()
        {
            Assemblies = GetAssemblies();
        }

        #region 选项
        /// <summary>
        /// 获取选项
        /// </summary>
        /// <typeparam name="TOptions">强类型选项类</typeparam>
        /// <param name="jsonKey">配置中对应的Key</param>
        /// <returns>TOptions</returns>
        public static TOptions GetOptions<TOptions>(string jsonKey)
            where TOptions : class, new()
        {
            return Configuration.GetSection(jsonKey).Get<TOptions>();
        }

        /// <summary>
        /// 获取选项副本
        /// </summary>
        /// <typeparam name="TOptions"></typeparam>
        /// <returns></returns>
        internal static TOptions GetDuplicateOptions<TOptions>()
            where TOptions : class, new()
        {
            return GetDuplicateService<IOptions<TOptions>>().Value;
        }
        #endregion

        #region 服务
        /// <summary>
        /// 获取请求生命周期的服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService GetService<TService>()
            where TService : class
        {
            return GetService(typeof(TService)) as TService;
        }

        /// <summary>
        /// 获取请求生命周期的服务
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetService(Type type)
        {
            return HttpContextUtility.GetCurrentHttpContext()?.RequestServices?.GetService(type);
        }

        /// <summary>
        /// 获取请求生命周期的服务
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static TService GetRequiredService<TService>()
            where TService : class
        {
            return GetRequiredService(typeof(TService)) as TService;
        }

        /// <summary>
        /// 获取请求生命周期的服务
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static object GetRequiredService(Type type)
        {
            return HttpContextUtility.GetCurrentHttpContext()?.RequestServices?.GetRequiredService(type);
        }
        /// <summary>
        /// 获取服务副本
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        internal static TService GetDuplicateService<TService>()
            where TService : class
        {
            return ServiceProvider.GetService<TService>();
        }
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

            // 读取应用配置
            var settings = GetOptions<AppSettingsOptions>("AppSettings") ?? new AppSettingsOptions { };

            var dependencyContext = DependencyContext.Default;

            // 读取项目程序集或 Furion 官方发布的包，或手动添加引用的dll
            var scanAssemblies = dependencyContext.CompileLibraries
                .Where(u => (u.Type == "project" && !excludeAssemblyNames.Any(j => u.Name.EndsWith(j)))
                    || (u.Type == "package" && u.Name.StartsWith(nameof(QS)))
                    || (settings.EnabledReferenceAssemblyScan == true && u.Type == "reference"))    // 判断是否启用引用程序集扫描
                .Select(u => AssemblyLoadContext.Default.LoadFromAssemblyName(new AssemblyName(u.Name)))
                .ToList();

            // 加载 `appsetting.json` 配置的外部程序集
            if (settings.ExternalAssemblies != null && settings.ExternalAssemblies.Any())
            {
                foreach (var externalAssembly in settings.ExternalAssemblies)
                {
                    scanAssemblies.Add(Assembly.Load(externalAssembly));
                }
            }

            return scanAssemblies;
        }

    }
}
