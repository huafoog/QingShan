using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.OpenApi.Models;
using QingShan.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace QingShan.Core.SpecificationDocument
{
    [SkipScan]
    internal static class SpecificationDocumentBuilder
    {
        /// <summary>
        /// 规范化文档配置
        /// </summary>
        public static SpecificationDocumentSettingsOptions _specificationDocumentSettings { get; set; }
        /// <summary>
        /// 文档默认分组
        /// </summary>
        private static IEnumerable<GroupOrder> _defaultGroups;

        /// <summary>
        /// 文档分组列表
        /// </summary>
        private static IEnumerable<string> _groups;
        static SpecificationDocumentBuilder()
        {
            GetControllerGroupsCached = new ConcurrentDictionary<Type, IEnumerable<GroupOrder>>();
            //初始化分组
            GetGroupOpenApiInfoCached = new ConcurrentDictionary<string, SpecificationOpenApiInfo>();
        }
        public static void Init(SpecificationDocumentSettingsOptions specificationDocumentSettings)
        {
            _specificationDocumentSettings = specificationDocumentSettings;
            // 默认分组，支持多个逗号分割
            _defaultGroups = new List<GroupOrder> { ResolveGroupOrder(_specificationDocumentSettings.DefaultGroupName) };
            // 加载所有分组
            _groups = ReadGroups();
        }

        /// <summary>
        /// 创建分组文档
        /// </summary>
        /// <param name="swaggerGenOptions">Swagger生成器对象</param>
        private static void CreateSwaggerDocs(SwaggerGenOptions swaggerGenOptions)
        {
            foreach (var group in _groups)
            {
                var groupOpenApiInfo = GetGroupOpenApiInfo(group) as OpenApiInfo;
                swaggerGenOptions.SwaggerDoc(group, groupOpenApiInfo);
            }
        }

        /// <summary>
        /// 构建Swagger全局配置
        /// </summary>
        /// <param name="swaggerOptions">Swagger 全局配置</param>
        internal static void BuildGen(SwaggerGenOptions swaggerOptions)
        {
            swaggerOptions.SwaggerGeneratorOptions.DescribeAllParametersInCamelCase = true;
            LoadXmlComments(swaggerOptions);
            ConfigureSecurities(swaggerOptions);
            CreateSwaggerDocs(swaggerOptions);
        }
        #region 配置授权
        /// <summary>
        /// 配置授权
        /// </summary>
        /// <param name="swaggerGenOptions">Swagger 生成器配置</param>
        private static void ConfigureSecurities(SwaggerGenOptions swaggerGenOptions)
        {
            // 判断是否启用了授权
            if (_specificationDocumentSettings.EnableAuthorized != true || _specificationDocumentSettings.SecurityDefinitions.Length == 0) return;

            var openApiSecurityRequirement = new OpenApiSecurityRequirement();
            swaggerGenOptions.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                });
            // 生成安全定义
            foreach (var securityDefinition in _specificationDocumentSettings.SecurityDefinitions)
            {
                // Id 必须定义
                if (string.IsNullOrEmpty(securityDefinition.Id)) continue;

                // 添加安全定义
                var openApiSecurityScheme = securityDefinition as OpenApiSecurityScheme;
                swaggerGenOptions.AddSecurityDefinition(securityDefinition.Id, openApiSecurityScheme);

                // 添加安全需求
                var securityRequirement = securityDefinition.Requirement;

                if (securityRequirement.Scheme.Reference != null)
                {
                    securityRequirement.Scheme.Reference.Id ??= securityDefinition.Id;
                    openApiSecurityRequirement.Add(securityRequirement.Scheme, securityRequirement.Accesses);
                }
            }

            // 添加安全需求
            if (openApiSecurityRequirement.Count > 0)
            {
                swaggerGenOptions.AddSecurityRequirement(openApiSecurityRequirement);
            }
        }
        #endregion

        #region 加载注释
        /// <summary>
        /// 加载注释描述文件
        /// </summary>
        /// <param name="swaggerGenOptions">Swagger 生成器配置</param>
        private static void LoadXmlComments(SwaggerGenOptions swaggerGenOptions)
        {
            var xmlComments = _specificationDocumentSettings.XmlComments;
            foreach (var xmlComment in xmlComments)
            {
                var assemblyXmlName = xmlComment.EndsWith(".xml") ? xmlComment : $"{xmlComment}.xml";
                var assemblyXmlPath = Path.Combine(AppContext.BaseDirectory, assemblyXmlName);
                if (File.Exists(assemblyXmlPath))
                {
                    swaggerGenOptions.IncludeXmlComments(assemblyXmlPath, true);
                }
            }
        }
        #endregion

        /// <summary>
        /// 读取所有分组信息
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<string> ReadGroups()
        {
            // 获取所有的控制器和动作方法
            var controllers = AppAssembly.CanBeScanTypes.Where(u => u.IsController());
            var actions = controllers.SelectMany(c => c.GetMethods().Where(u => IsAction(u, c)));

            // 合并所有分组
            var groupOrders = controllers.SelectMany(u => GetControllerGroups(u))
                //.Union(
                //    actions.SelectMany(u => GetActionGroups(u))
                //)
                .Where(u => u != null)
                // 分组后取最大排序
                .GroupBy(u => u.Group)
                .Select(u => new GroupOrder
                {
                    Group = u.Key,
                    Order = u.Max(x => x.Order)
                });

            // 分组排序
            return groupOrders
                .OrderByDescending(u => u.Order)
                .ThenBy(u => u.Group)
                .Select(u => u.Group);
        }
        /// <summary>
        /// </summary>
        private static readonly ConcurrentDictionary<Type, IEnumerable<GroupOrder>> GetControllerGroupsCached;
        /// <summary>
        /// 获取控制器分组列表
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        private static IEnumerable<GroupOrder> GetControllerGroups(Type type)
        {
            var isCached = GetControllerGroupsCached.TryGetValue(type, out var groups);
            if (isCached) return groups;

            // 本地函数
            static IEnumerable<GroupOrder> GetGroupOrder(Type type)
            {
                // 如果控制器没有定义 [ApiDescriptionSettings] 特性，则返回默认分组
                if (!type.IsDefined(typeof(ApiDescriptionSettingsAttribute), true)) return _defaultGroups;

                // 读取分组
                var apiDescriptionSettings = type.GetCustomAttribute<ApiDescriptionSettingsAttribute>(true);
                if (apiDescriptionSettings.Groups == null || apiDescriptionSettings.Groups.Length == 0) return _defaultGroups;

                // 处理排序
                var groupOrders = new List<GroupOrder>();
                foreach (var group in apiDescriptionSettings.Groups)
                {
                    groupOrders.Add(ResolveGroupOrder(group));
                }

                return groupOrders;
            }

            // 调用本地函数
            groups = GetGroupOrder(type);
            GetControllerGroupsCached.TryAdd(type, groups);
            return groups;
        }

        /// <summary>
        /// 解析分组名称中的排序
        /// </summary>
        /// <param name="group">分组名</param>
        /// <returns></returns>
        private static GroupOrder ResolveGroupOrder(string group)
        {
            var order = 0;

            var groupOpenApiInfo = GetGroupOpenApiInfo(group);
            return new GroupOrder
            {
                Group = group,
                Order = groupOpenApiInfo.Order ?? order
            };
        }
        /// <summary>
        /// </summary>
        private static readonly ConcurrentDictionary<string, SpecificationOpenApiInfo> GetGroupOpenApiInfoCached;
        /// <summary>
        /// 获取分组配置信息
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private static SpecificationOpenApiInfo GetGroupOpenApiInfo(string group)
        {
            var isCached = GetGroupOpenApiInfoCached.TryGetValue(group, out var specificationOpenApiInfo);
            if (isCached) return specificationOpenApiInfo;

            // 本地函数
            static SpecificationOpenApiInfo Function(string group)
            {
                return _specificationDocumentSettings.GroupOpenApiInfos.FirstOrDefault(u => u.Group == group) ?? new SpecificationOpenApiInfo { Group = group };
            }

            // 调用本地函数
            specificationOpenApiInfo = Function(group);
            GetGroupOpenApiInfoCached.TryAdd(group, specificationOpenApiInfo);
            return specificationOpenApiInfo;
        }

        /// <summary>
        /// 是否是动作方法
        /// </summary>
        /// <param name="method">方法</param>
        /// <param name="declaringType">声明类型</param>
        /// <returns></returns>
        private static bool IsAction(MethodInfo method, Type declaringType)
        {
            // 不是非公开、抽象、静态、泛型方法
            if (!method.IsPublic || method.IsAbstract || method.IsStatic || method.IsGenericMethod) return false;

            // 如果所在类型不是控制器，则该行为也被忽略
            if (method.DeclaringType != declaringType) return false;

            // 不是能被导出忽略的接方法
            if (method.IsDefined(typeof(ApiExplorerSettingsAttribute), true) && method.GetCustomAttribute<ApiExplorerSettingsAttribute>(true).IgnoreApi) return false;

            return true;
        }

        #region UI
        /// <summary>
        /// Swagger UI 构建
        /// </summary>
        /// <param name="swaggerUIOptions"></param>
        /// <param name="routePrefix">api地址</param>
        internal static void BuildUI(SwaggerUIOptions swaggerUIOptions,string routePrefix =default)
        {
            // 配置分组终点路由
            CreateGroupEndpoint(swaggerUIOptions);

            // 配置文档标题
            swaggerUIOptions.DocumentTitle = _specificationDocumentSettings.DocumentTitle??"1234";

            // 配置UI地址
            swaggerUIOptions.RoutePrefix = routePrefix ?? _specificationDocumentSettings.RoutePrefix;

            // 文档展开设置
            swaggerUIOptions.DocExpansion(_specificationDocumentSettings.DocExpansionState.Value);
        }
        /// <summary>
        /// 配置分组终点路由
        /// </summary>
        /// <param name="swaggerUIOptions"></param>
        private static void CreateGroupEndpoint(SwaggerUIOptions swaggerUIOptions)
        {
            foreach (var group in _groups)
            {
                var groupOpenApiInfo = GetGroupOpenApiInfo(group);

                swaggerUIOptions.SwaggerEndpoint($"/swagger/{group}/swagger.json", groupOpenApiInfo?.Group ?? group);
            }
        }

        /// <summary>
        /// 构建Swagger全局配置
        /// </summary>
        /// <param name="swaggerOptions">Swagger 全局配置</param>
        internal static void Build(SwaggerOptions swaggerOptions)
        {
            // 生成V2版本
            swaggerOptions.SerializeAsV2 = _specificationDocumentSettings.FormatAsV2 == true;
        }
        #endregion
    }
}
