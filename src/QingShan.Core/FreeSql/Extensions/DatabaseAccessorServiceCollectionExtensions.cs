using FreeSql;
using Microsoft.Extensions.DependencyInjection.Extensions;
using QingShan.Core;
using QingShan.Core.FreeSql;
using QingShan.Core.FreeSql.Options;
using QingShan.DatabaseAccessor;
using QingShan.DependencyInjection;
using System;
using System.Linq;

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
            services.AddConfigurableOptions<DatabaseAccessorSettingsOptions>();
            var dbConfig = App.GetDefultOptions<DatabaseAccessorSettingsOptions>();

            var connectionString = $"Data Source={dbConfig.Host};Port={dbConfig.Port};{$"User ID={dbConfig.User};".IF(dbConfig.User.NotNull())}{$"Password={dbConfig.Password};".IF(dbConfig.Password.NotNull())} Initial Catalog={dbConfig.Database};{dbConfig.Extension}";
            var freeSqlBuilder = new FreeSqlBuilder()
                    .UseConnectionString(dbConfig.Type, connectionString)
# if DEBUG
                    .UseAutoSyncStructure(dbConfig.SyncStructure)
# endif
                    .UseLazyLoading(false)
                    .UseNoneCommandParameter(true);
            var fsql = freeSqlBuilder.Build();
            // 注册FreeSql   IFreeSql必须使用单例注入
            services.AddSingleton<IFreeSql>(fsql);
            if (dbConfig.GlobalFilter)
            {
                //全局过滤
                fsql.GlobalFilter.Apply<ISoftDeletable>("DeleteTime", a => !a.DeleteTime.HasValue);
            }



#if DEBUG
            if (dbConfig.PrintingSQL)
            {
                //监听生成的sql语句
                fsql.Aop.CurdBefore += (s, e) =>
                {
                    Console.WriteLine(e.Sql);
                };
            }
#endif

            if (dbConfig.ReturnCreateSql)
             {
                var entities = App.CanBeScanTypes.Where(o => o.IsEntityType());
                Console.WriteLine(fsql.CodeFirst.GetComparisonDDLStatements(entities.ToArray()));
            }

            //fsql.CodeFirst.GetComparisonDDLStatements<Topic>()

            services.TryAddScoped(typeof(IRepository<>), typeof(Repository<>));
            // 注册仓储
            services.TryAddScoped(typeof(IKeyRepository<,>), typeof(KeyRepository<,>));
            services.AddScoped<UnitOfWorkManager>();


            configure?.Invoke(services);
            return services;
        }

    }
}
