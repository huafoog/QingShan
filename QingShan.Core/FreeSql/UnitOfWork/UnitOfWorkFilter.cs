﻿//using Microsoft.AspNetCore.Mvc.Controllers;
//using Microsoft.AspNetCore.Mvc.Filters;
//using QingShan.DependencyInjection;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using System.Text;
//using System.Threading.Tasks;
//using System.Transactions;

//namespace QingShan.DatabaseAccessor
//{
//    /// <summary>
//    /// 工作单元拦截器
//    /// </summary>
//    [SkipScan]
//    internal sealed class UnitOfWorkFilter : IAsyncActionFilter, IOrderedFilter
//    {
//        /// <summary>
//        /// MiniProfiler 分类名
//        /// </summary>
//        private const string MiniProfilerCategory = "transaction";

//        /// <summary>
//        /// 过滤器排序
//        /// </summary>
//        internal const int FilterOrder = -1000;

//        /// <summary>
//        /// 数据库上下文池
//        /// </summary>
//        private readonly IDbContextPool _dbContextPool;

//        /// <summary>
//        /// 构造函数
//        /// </summary>
//        /// <param name="dbContextPool">数据库上下文池</param>
//        public UnitOfWorkFilter(IDbContextPool dbContextPool)
//        {
//            _dbContextPool = dbContextPool;
//        }

//        public int Order => FilterOrder;

//        /// <summary>
//        /// 拦截请求
//        /// </summary>
//        /// <param name="context">动作方法上下文</param>
//        /// <param name="next">中间件委托</param>
//        /// <returns></returns>
//        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
//        {
//            // 获取动作方法描述器
//            var actionDescriptor = context.ActionDescriptor as ControllerActionDescriptor;
//            var method = actionDescriptor.MethodInfo;

//            // 如果方法贴了 [NonTransact] 则跳过事务
//            var disabledTransact = method.IsDefined(typeof(NonTransactAttribute), true);

//            // 打印验禁止事务信息
//            if (disabledTransact) App.PrintToMiniProfiler(MiniProfilerCategory, "Disabled !");

//            // 判断是否支持环境事务
//            var isSupportTransactionScope = _dbContextPool.GetDbContexts().Any(u => !DatabaseProvider.NotSupportTransactionScopeDatabase.Contains(u.Database.ProviderName));
//            TransactionScope transaction = null;

//            if (isSupportTransactionScope && !disabledTransact)
//            {
//                // 打印事务开始消息
//                App.PrintToMiniProfiler(MiniProfilerCategory, "Beginning");

//                // 获取工作单元特性
//                UnitOfWorkAttribute unitOfWorkAttribute = null;
//                if (!method.IsDefined(typeof(UnitOfWorkAttribute), true)) unitOfWorkAttribute ??= new UnitOfWorkAttribute();
//                else unitOfWorkAttribute = method.GetCustomAttribute<UnitOfWorkAttribute>();

//                // 开启分布式事务
//                transaction = new TransactionScope(unitOfWorkAttribute.ScopeOption
//              , new TransactionOptions { IsolationLevel = unitOfWorkAttribute.IsolationLevel }
//              , unitOfWorkAttribute.AsyncFlowOption);
//            }
//            // 打印不支持事务
//            else if (!isSupportTransactionScope && !disabledTransact) { App.PrintToMiniProfiler(MiniProfilerCategory, "NotSupported !"); }

//            // 继续执行
//            var resultContext = await next();

//            // 判断是否出现异常
//            if (resultContext.Exception == null)
//            {
//                // 将所有上下文提交事务
//                var hasChangesCount = await _dbContextPool.SavePoolNowAsync();

//                if (isSupportTransactionScope && !disabledTransact)
//                {
//                    transaction?.Complete();
//                    transaction?.Dispose();

//                    // 打印事务提交消息
//                    App.PrintToMiniProfiler(MiniProfilerCategory, "Completed", $"Transaction Completed! Has {hasChangesCount} DbContext Changes.");
//                }
//            }
//            else
//            {
//                // 打印事务回滚消息
//                if (isSupportTransactionScope && !disabledTransact) App.PrintToMiniProfiler(MiniProfilerCategory, "Rollback", isError: true);
//            }
//        }
//    }
//}
