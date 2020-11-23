using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using QS.DataLayer.Entities;
using System.Transactions;

namespace QS.Core.Web.Filter.Transaction
{
    /// <summary>
    /// 自动事务提交过滤器，在<see cref="ActionFilterAttribute.OnResultExecutionAsync(ResultExecutingContext, ResultExecutionDelegate)"/>方法中执行<see cref="EFContext.Database.BeginTransactionAsync()"/>进行事务提交
    /// <para>继承自<see cref="ServiceFilterAttribute"/>在过滤器<see cref="IAsyncActionFilter"/>中使用依赖注入</para>
    /// </summary>
    public class TransactionInterceptorAttribute : ServiceFilterAttribute
    {
        public TransactionInterceptorAttribute() : base(typeof(TransactionInterceptorFilterImpl))
        {

        }

        /// <summary>
        /// 构造函数
        /// <para>支持传入 事务级别 <see cref="IsolationLevel"/> 参数值</para>
        /// </summary>
        /// <param name="isolationLevel">事务隔离级别</param>
        public TransactionInterceptorAttribute(IsolationLevel isolationLevel) :this()
        {
            IsolationLevel = isolationLevel;
        }
        /// <summary>
        /// <para>支持传入 事务范围 <see cref="TransactionScope"/>，事务级别 <see cref="IsolationLevel"/> 参数值</para>
        /// </summary>
        /// <param name="scopeOption">事务范围</param>
        /// <param name="isolationLevel">事务隔离级别</param>
        public TransactionInterceptorAttribute(TransactionScopeOption scopeOption, IsolationLevel isolationLevel) : this()
        {
            ScopeOption = scopeOption;
            IsolationLevel = isolationLevel;
        }

        /// <summary>
        /// 事务范围
        /// </summary>
        /// <remarks>
        /// <para>默认：<see cref="TransactionScopeOption.Required"/>，参见：<see cref="TransactionScope"/></para>
        /// <para>说明：如果当前上下文已存在事务，那么这个事务范围将加入已有的事务。否则，它将创建自己的事务</para>
        /// </remarks>
        public TransactionScopeOption ScopeOption { get; set; } = TransactionScopeOption.Required;

        /// <summary>
        /// 事务隔离级别
        /// </summary>
        /// <remarks>
        /// <para>默认：<see cref="IsolationLevel.ReadCommitted"/>，参见：<see cref="IsolationLevel"/></para>
        /// <para>说明：当事务A更新某条数据的时候，不容许其他事务来更新该数据，但可以进行读取操作</para>
        /// </remarks>
        public IsolationLevel IsolationLevel { get; set; } = IsolationLevel.ReadCommitted;

        /// <summary>
        /// 跨线程异步流
        /// </summary>
        /// <remarks>
        /// <para>默认：<see cref="TransactionScopeAsyncFlowOption.Enabled"/>，参见：<see cref="TransactionScopeAsyncFlowOption"/></para>
        /// <para>说明：允许跨线程连续任务的事务流，如有异步操作需开启该选项</para>
        /// </remarks>
        public TransactionScopeAsyncFlowOption AsyncFlowOption { get; set; } = TransactionScopeAsyncFlowOption.Enabled;
    }
}
