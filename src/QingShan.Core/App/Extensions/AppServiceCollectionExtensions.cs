using QingShan.DependencyInjection;
using System;
using Microsoft.Extensions.Configuration;
using QingShan.Core.Filter;
using QingShan.Attributes;
using Newtonsoft.Json.Serialization;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 应用服务集合拓展类
    /// </summary>
    [SkipScan]
    public static class AppServiceCollectionExtensions
    {
        /// <summary>
        /// 默认的控制器服务
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <returns>IMvcBuilder</returns>
        public static IServiceCollection AddDefaultController(this IServiceCollection services)
        {
            services.AddControllers(o =>
            {
                //全局异常
                o.Filters.Add<GlobalExceptionFilter>();
                //注册模型验证过滤器到全局
                o.Filters.Add<ApiResponseFilterAttribute>();
            }).AddNewtonsoftJson(
               options =>
               {
                   options.SerializerSettings.ContractResolver = new DefaultContractResolver()
                   {
                       NamingStrategy = new CamelCaseNamingStrategy()
                   };
                   options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
               }
           ).ConfigureApiBehaviorOptions(option =>
           {
               //关闭默认模型验证
               option.SuppressModelStateInvalidFilter = true;
           });
            return services;
        }

        /// <summary>
        /// 添加服务
        /// <para>服务包含 配置 依赖注入 缓存 数据库 跨域</para>
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">配置</param>
        /// <param name="configuration">服务配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddInject(this IServiceCollection services, IConfiguration configuration, Action<IServiceCollection> configure = null)
        {
            // 添加 HttContext 访问器
            services.AddHttpContextAccessor();
            // 注册全局依赖注入
            services.AddDependencyInjection();
            services.AddCache(configuration);
            services.AddDatabaseAccessor(configuration);
            services.AddCorsAccessor(configuration);
            services.AddDefaultController();
            // 自定义服务
            configure?.Invoke(services);
            return services;
        }

        /// <summary>
        /// 添加应用配置
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">配置</param>
        /// <param name="configuration">服务配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddApp<JwtHandler>(this IServiceCollection services, IConfiguration configuration, Action<IServiceCollection> configure = null)
            where JwtHandler : class, AspNetCore.Authorization.IAuthorizationHandler
        {
            // 添加 HttContext 访问器
            services.AddHttpContextAccessor();
            // 注册全局依赖注入
            services.AddDependencyInjection();
            services.AddSpecificationDocuments(configuration);
            services.AddCache(configuration);
            services.AddDatabaseAccessor(configuration);
            services.AddStaticFile(configuration);
            services.AddRateLimit(configuration);
            services.AddCorsAccessor(configuration);
            services.AddJwt<JwtHandler>(enableGlobalAuthorize: true);
            services.AddDefaultController();
            // 自定义服务
            configure?.Invoke(services);
            return services;
        }
    }
}