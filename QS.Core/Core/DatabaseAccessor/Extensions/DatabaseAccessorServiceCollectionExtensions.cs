using FreeSql;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QS.Core;
using QS.Core.DatabaseAccessor;
using QS.Core.DependencyInjection;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 数据库访问器服务拓展类
    /// </summary>
    [SkipScan]
    public static class DatabaseAccessorServiceCollectionExtensions
    {
        /// <summary>
        /// 添加数据库上下文
        /// </summary>
        /// <param name="services">服务集合</param>
        /// <param name="configure">配置</param>
        /// <returns>服务集合</returns>
        public static IServiceCollection AddDatabaseAccessor(this IServiceCollection services, Action<IServiceCollection> configure = null)
        {

            var dbConfig = App.GetOptions<DatabaseAccessorSettingsOptions>();

            var freeSqlBuilder = new FreeSqlBuilder()
                    .UseConnectionString(dbConfig.Type, dbConfig.ConnectionString)
# if DEBUG
                    .UseAutoSyncStructure(dbConfig.SyncStructure)
# endif
                    .UseLazyLoading(false)
                    .UseNoneCommandParameter(true);
            var fsql = freeSqlBuilder.Build();
            // 注册FreeSql
            services.AddSingleton<IFreeSql>(fsql);

            if (App.Settings.InjectMiniProfiler??false)
            {
                // 监听生成的sql语句
                fsql.Aop.CurdBefore += (s, e) =>
                {
                    App.PrintToMiniProfiler(MiniProfilerCategory.MINI_PROFILER_SQL, "Information", e.Sql);
                };
            }
            // 注册仓储
            services.TryAddScoped(typeof(IRepository<,>), typeof(Repository<,>));
            services.AddScoped<UnitOfWorkManager>();
            services.TryAddScoped(typeof(TransactionInterceptorFilterImpl));
            services.TryAddScoped(typeof(TransactionInterceptorAttribute));
            return services;
        }
    }
}
