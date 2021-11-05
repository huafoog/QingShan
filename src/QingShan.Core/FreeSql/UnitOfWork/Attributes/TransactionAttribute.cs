using FreeSql;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Data;
using System.Threading.Tasks;

namespace QingShan.Core.FreeSql.UnitOfWork.Attributes
{
    /// <summary>
    /// 事务筛选起
    /// </summary>
    [AttributeUsage(AttributeTargets.Class| AttributeTargets.Method)]
    public class TransactionAttribute : UnitOfWorkAttribute, IAsyncActionFilter
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public TransactionAttribute()
        {
        }

        /// <summary>
        /// 构造函数
        /// <para>支持传入 事务级别 <see cref="IsolationLevel"/> 参数值</para>
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别</param>
        public TransactionAttribute(IsolationLevel isolationLevel):base(isolationLevel)
        {
        }
        /// <summary>
        /// <para>支持传入 事务传播方式 <see cref="Propagation"/>，事务级别 <see cref="IsolationLevel"/> 参数值</para>
        /// </summary>
        /// <param name="propagation">事务传播方式</param>
        /// <param name="isolationLevel">事务隔离级别</param>
        public TransactionAttribute(Propagation propagation, IsolationLevel isolationLevel):base(propagation, isolationLevel)
        {
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            await TransactionAsync(context.HttpContext.RequestServices, next);
        }
    }
}
