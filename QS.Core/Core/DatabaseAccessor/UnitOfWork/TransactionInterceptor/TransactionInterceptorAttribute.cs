using FreeSql;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Data;

namespace QS.Core.DatabaseAccessor
{
    /// <summary>
    /// 自动事务提交过滤器，在<see cref="ActionFilterAttribute.OnResultExecutionAsync(ResultExecutingContext, ResultExecutionDelegate)"/>方法中执行拦截进行事务提交
    /// <para>继承自<see cref="ServiceFilterAttribute"/>在过滤器<see cref="IAsyncActionFilter"/>中使用依赖注入</para>
    /// </summary>
    public class TransactionInterceptorAttribute : ServiceFilterAttribute
    {
        public TransactionInterceptorAttribute() : base(typeof(TransactionInterceptorFilterImpl))
        {
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
        public TransactionInterceptorAttribute(Propagation propagation, IsolationLevel isolationLevel) : this()
        {
            Propagation = propagation;
            IsolationLevel = isolationLevel;
        }
    }
}
