using FreeSql;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using QingShan.DependencyInjection;
using System;
using System.Data;
using System.Threading.Tasks;

namespace QingShan.Core.FreeSql.UnitOfWork.Attributes
{
    /// <summary>
    /// 工作单元配置特性
    /// </summary>
    /// <remarks>
    /// <para>支持配置事务范围、隔离级别</para>
    /// </remarks>
    [SkipScan, AttributeUsage(AttributeTargets.Method| AttributeTargets.Class)]
    public abstract class UnitOfWorkAttribute : Attribute
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public UnitOfWorkAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// <para>支持传入 事务级别 <see cref="IsolationLevel"/> 参数值</para>
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别</param>
        public UnitOfWorkAttribute(IsolationLevel isolationLevel)
        {
            IsolationLevel = isolationLevel;
        }
        /// <summary>
        /// <para>支持传入 事务传播方式 <see cref="Propagation"/>，事务级别 <see cref="IsolationLevel"/> 参数值</para>
        /// </summary>
        /// <param name="propagation">事务传播方式</param>
        /// <param name="isolationLevel">事务隔离级别</param>
        public UnitOfWorkAttribute(Propagation propagation, IsolationLevel isolationLevel)
        {
            Propagation = propagation;
            IsolationLevel = isolationLevel;
        }


        /// <summary>
        /// 事务传播方式
        /// <para>默认方式为<see cref="Propagation.Required"/></para>
        /// </summary>
        public Propagation Propagation { get; set; } = Propagation.Required;

        /// <summary>
        /// 事务隔离级别
        /// </summary>
        /// <remarks>
        /// <para>默认：<see cref="IsolationLevel.ReadCommitted"/>，参见：<see cref="IsolationLevel"/></para>
        /// <para>说明：当事务A更新某条数据的时候，不容许其他事务来更新该数据，但可以进行读取操作</para>
        /// </remarks>
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;


        /// <summary>
        /// 事务提交方法
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="next"></param>
        /// <returns></returns>
        protected virtual async Task TransactionAsync(IServiceProvider serviceProvider, ActionExecutionDelegate next)
        {
            var _unitOfWorkManager = serviceProvider.GetRequiredService<UnitOfWorkManager>();
            var _logger = serviceProvider.GetRequiredService<ILogger<UnitOfWorkAttribute>>();

            //事务
            using var trans = _unitOfWorkManager.Begin(Propagation, IsolationLevel);
            // 继续执行
            var resultContext = await next();
            // 判断是否出现异常
            if (resultContext.Exception == null)
            {
                trans.Commit();
            }
            else
            {
                trans.Rollback();
                _logger.LogError(resultContext.Exception, "提交事务出错");
                throw resultContext.Exception;
            }

        }

    }
}